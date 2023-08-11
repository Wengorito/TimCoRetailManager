using Caliburn.Micro;
using System;
using System.ComponentModel;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<string> _products;

        public BindingList<string> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
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
