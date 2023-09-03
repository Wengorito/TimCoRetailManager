using System.ComponentModel.DataAnnotations;

namespace TRMWebUI_NET4._7._2.Models
{
    public class ProductViewModel
    {
        /// <summary>
        /// Unique identifier for products model.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the product available for selling.
        /// </summary>
        // Tymczasowe rozwiązanie, generalnie unikamy modyfikacji istniejących klas
        // TODO erase the decoration
        [Display(Name = "Nazwa produktu")]
        public string ProductName { get; set; }
        /// <summary>
        /// Description of the item available in products.
        /// </summary>
        [Display(Name = "Opis")]
        public string Description { get; set; }
        /// <summary>
        /// The retail price is the final price that a good is sold to customers for.
        /// </summary>
        [Display(Name = "Cena jednostkowa")]
        public decimal RetailPrice { get; set; }
        /// <summary>
        /// Remaining items in the stock.
        /// </summary>
        [Display(Name = "Stan magazynowy")]
        public int QuantityInStock { get; set; }

        [Display(Name = "VAT")]
        public bool IsTaxable { get; set; }

        [Display(Name = "Cena jednostkowa")]
        public string PriceToDisplay
        {
            get
            {
                return string.Format("{0:C}", RetailPrice);
            }
        }
    }
}