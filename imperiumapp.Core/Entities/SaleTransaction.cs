namespace imperiumapp.Core.Entities
{
    public class SaleTransaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; } // قيمة الفاتورة
        public string PaymentMethod { get; set; } = string.Empty; // Cash أو Debt

        // ربط العملية بالمنتج اللي انباع
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int QuantitySold { get; set; } // كم حبة انباع

        // إذا كان الدفع "دين" (Debt)، لازم نربطه بالمشترك لنعرف مين اللي عليه المصاري
        public int? MemberId { get; set; }
        public Member? Member { get; set; }
    }
}