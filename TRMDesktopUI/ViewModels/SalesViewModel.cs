using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<ProductModel> _products;
        private IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var products = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(products);

        }

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<ProductModel> _cart;

        public BindingList<ProductModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private int _itemQuantitiy;

        public int ItemQuantity
        {
            get { return _itemQuantitiy; }
            set
            {
                _itemQuantitiy = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public string SubTotal
        {
            get
            {
                return "$0.00";
            }
        }

        public string Tax
        {
            get
            {
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                return "$0.00";
            }
        }

        public bool CanAddtoCart
        {
            get
            {
                // Make sure selected
                // Make sure quantity

                return false;
            }
        }

        public void AddToCart()
        {
            throw new NotImplementedException();
        }

        public bool CanRemoveFromCart
        {
            get
            {
                // Make sure selected

                return false;
            }
        }

        public void RemoveFromCart()
        {
            throw new NotImplementedException();
        }

        public bool CanCheckOut
        {
            get
            {
                // Make sure selected

                return false;
            }
        }

        public void CheckOut()
        {
            throw new NotImplementedException();
        }

    }
}
