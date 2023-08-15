using System;

namespace TRMDataManager.Library.Models
{
    public class SaleDBModel
    {
        public string CashierId { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

    }
}
