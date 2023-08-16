namespace TRMDesktopUI.Library.Models
{
    public class CartItemModel
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
            }
        }
    }
}
