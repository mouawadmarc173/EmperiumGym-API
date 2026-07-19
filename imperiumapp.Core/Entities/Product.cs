namespace imperiumapp.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int Stock { get; set; }
        public int StockQuantity { get; set; } // لإرضاء الداتابيز

        public int AlertLimit { get; set; }
        public int LowStockAlert { get; set; } // 👈 العمود القديم اللي عم يعملنا الإيرور هلق!

        public string Category { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
    }
}