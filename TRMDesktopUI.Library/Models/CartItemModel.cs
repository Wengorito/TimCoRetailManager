using Caliburn.Micro;

namespace TRMDesktopUI.Library.Models
{
    public class CartItemModel : PropertyChangedBase
    {
        public ProductModel Product { get; set; }

        private int _quantityInCart;
        public int QuantityInCart
        {
            get
            {
                return _quantityInCart;
            }
            set
            {
                _quantityInCart = value;
                NotifyOfPropertyChange(() => DisplayText);
            }
        }

        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName} ({QuantityInCart})";
            }
        }
    }
}
