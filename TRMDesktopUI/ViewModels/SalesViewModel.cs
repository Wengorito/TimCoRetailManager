using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Helpers;
using TRMDesktopUI.Library.Models;
using TRMDesktopUI.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<ProductDisplayModel> _products;
        private IProductEndpoint _productEndpoint;
        private ISaleEndpoint _saleEndpoint;
        private readonly IMapper _mapper;
        private readonly StatusInfoViewModel _statusInfo;
        private readonly IWindowManager _windowManager;
        private readonly IConfigHelper _configHelper;

        public SalesViewModel(
            IConfigHelper configHelper,
            IProductEndpoint productEndpoint,
            ISaleEndpoint saleEndpoint,
            IMapper mapper,
            StatusInfoViewModel status,
            IWindowManager windowManager)
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
            _saleEndpoint = saleEndpoint;
            _mapper = mapper;
            _statusInfo = status;
            _windowManager = windowManager;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadProducts();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                // To have multiple vms use IoC somehow 
                //var status = IoC.Get<StatusInfoViewModel>();
                if (ex.Message == "Unauthorized")
                {
                    _statusInfo.UpdateMessage("Unauthorized Access", "You do not have the permission to interact with the Sales Form");
                    await _windowManager.ShowDialogAsync(_statusInfo, null, settings);
                }
                else
                {
                    _statusInfo.UpdateMessage("Fatal Exception", ex.Message);
                    await _windowManager.ShowDialogAsync(_statusInfo, null, settings);
                }
                TryCloseAsync();
            }
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            var productsToDisplay = _mapper.Map<List<ProductDisplayModel>>(productList);
            Products = new BindingList<ProductDisplayModel>(productsToDisplay);

        }

        public BindingList<ProductDisplayModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductDisplayModel _selectedProduct;
        public ProductDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemDisplayModel> _cart = new BindingList<CartItemDisplayModel>();

        public BindingList<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private CartItemDisplayModel _selectedCartItem;
        public CartItemDisplayModel SelectedCartItem
        {
            get { return _selectedCartItem; }
            set
            {
                _selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }

        private int _itemQuantitiy = 1;

        public int ItemQuantity
        {
            get { return _itemQuantitiy; }
            set
            {
                _itemQuantitiy = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private async Task ResetSalesViewModel()
        {
            Cart = new BindingList<CartItemDisplayModel>();

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);

            await LoadProducts();
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;

            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityInCart;
            }

            return subTotal;
        }

        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate();

            taxAmount = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

            return taxAmount;
        }

        public string SubTotal
        {
            get
            {
                CultureInfo provider = new CultureInfo("pl-pl");
                return CalculateSubTotal().ToString("C", provider);
            }
        }

        public string Tax
        {
            get
            {
                CultureInfo provider = new CultureInfo("pl-pl");
                return CalculateTax().ToString("C", provider);
            }
        }

        public string Total
        {
            get
            {
                CultureInfo provider = new CultureInfo("pl-pl");
                return (CalculateSubTotal() + CalculateTax()).ToString("C", provider);
            }
        }

        public bool CanAddToCart
        {
            get
            {
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    return true;
                }

                return false;
            }
        }

        public void AddToCart()
        {
            var existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
            }
            else
            {
                CartItemDisplayModel item = new CartItemDisplayModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity,
                };
                Cart.Add(item);
            }

            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                if (ItemQuantity > 0 && SelectedCartItem?.QuantityInCart >= ItemQuantity)
                {
                    return true;
                }

                return false;
            }
        }

        public void RemoveFromCart()
        {
            SelectedCartItem.QuantityInCart -= ItemQuantity;
            SelectedCartItem.Product.QuantityInStock += ItemQuantity;
            ItemQuantity = 1;

            if (SelectedCartItem.QuantityInCart < 1)
            {
                Cart.Remove(SelectedCartItem);
            }

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanCheckOut
        {
            get
            {
                // Make sure selected
                if (Cart.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public async void CheckOut()
        {
            // Create a SaleModel and post to the API
            var sale = new SaleModel();

            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel
                {
                    ProductId = item.Product.Id,
                    Quantity = item.QuantityInCart
                });
            }

            await _saleEndpoint.PostSale(sale);

            await ResetSalesViewModel();
        }
    }
}
