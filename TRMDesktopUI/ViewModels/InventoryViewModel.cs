using AutoMapper;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Models;

namespace TRMDesktopUI.ViewModels
{
    public class InventoryViewModel : Screen
    {
        private BindingList<ProductDisplayModel> _products;
        private IProductEndpoint _productEndpoint;
        private ISaleEndpoint _saleEndpoint;
        private readonly IInventoryEndpoint _inventoryEndpoint;
        private readonly IMapper _mapper;
        private readonly StatusInfoViewModel _statusInfo;
        private readonly IWindowManager _windowManager;

        public InventoryViewModel(
            IProductEndpoint productEndpoint,
            ISaleEndpoint saleEndpoint,
            IInventoryEndpoint inventoryEndpoint,
            IMapper mapper,
            StatusInfoViewModel status,
            IWindowManager windowManager
            )
        {
            _productEndpoint = productEndpoint;
            _saleEndpoint = saleEndpoint;
            _mapper = mapper;
            _statusInfo = status;
            _windowManager = windowManager;
            _inventoryEndpoint = inventoryEndpoint;
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
                settings.Title = "Custom Error Display System";

                // To have multiple vms use IoC somehow 
                //var status = IoC.Get<StatusInfoViewModel>();
                if (ex.Message == "Unauthorized")
                {
                    _statusInfo.UpdateMessage("Unauthorized Access", "You do not have the permission to interact with the Inventory Form");
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
                NotifyOfPropertyChange(() => CanAddToInventory);
            }
        }

        private int _quantitiy;

        public int Quantity
        {
            get { return _quantitiy; }
            set
            {
                _quantitiy = value;
                NotifyOfPropertyChange(() => Quantity);
                NotifyOfPropertyChange(() => CanAddToInventory);
            }
        }

        private decimal _purchasePrice;

        public decimal PurchasePrice
        {
            get { return _purchasePrice; }
            set
            {
                _purchasePrice = value;
                NotifyOfPropertyChange(() => PurchasePrice);
                NotifyOfPropertyChange(() => CanAddToInventory);
            }
        }

        private DateTime _purchaseDate = DateTime.Now;

        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set
            {
                _purchaseDate = value;
                NotifyOfPropertyChange(() => PurchaseDate);
                NotifyOfPropertyChange(() => CanAddToInventory);
            }
        }

        private DateTime _datesAhead = DateTime.Now;

        public DateTime DatesAhead
        {
            get { return _datesAhead; }
        }

        private async Task ResetInventoryViewModel()
        {
            SelectedProduct = null;
            Quantity = 0;
            PurchasePrice = 0;
            PurchaseDate = DateTime.UtcNow;

            NotifyOfPropertyChange(() => Quantity);
            NotifyOfPropertyChange(() => PurchasePrice);
            NotifyOfPropertyChange(() => PurchaseDate);
            NotifyOfPropertyChange(() => CanAddToInventory);

            await LoadProducts();
        }

        public bool CanAddToInventory
        {
            get
            {
                if (SelectedProduct != null
                    && Quantity > 0
                    && PurchasePrice > 0
                    && PurchaseDate <= DateTime.Now)
                {
                    return true;
                }

                return false;
            }
        }

        public async void AddToInventory()
        {
            // Create a SaleModel and post to the API
            var purchase = new InventoryModel
            {
                ProductId = SelectedProduct.Id,
                Quantity = Quantity,
                PurchasePrice = PurchasePrice,
                PurchaseDate = PurchaseDate
            };

            await _inventoryEndpoint.PostPurchase(purchase);

            await ResetInventoryViewModel();
        }
    }
}
