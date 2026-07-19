using imperiumapp.Core.Entities;
using imperiumapp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace imperiumapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            // 1. تعبئة الباركود الوهمي
            if (string.IsNullOrEmpty(product.Barcode))
            {
                product.Barcode = "NO-BARCODE-" + Guid.NewGuid().ToString().Substring(0, 5);
            }

            // 2. إرضاء الداتابيز بأعمدتها القديمة
            product.StockQuantity = product.Stock;
            product.LowStockAlert = product.AlertLimit; // 👈 سكتنا الداتابيز هون!

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            existingProduct.Stock = product.Stock;
            existingProduct.StockQuantity = product.Stock; // تحديث القديم

            existingProduct.AlertLimit = product.AlertLimit;
            existingProduct.LowStockAlert = product.AlertLimit; // 👈 وتحديث القديم هون كمان

            existingProduct.Category = product.Category;

            if (!string.IsNullOrEmpty(product.Barcode))
            {
                existingProduct.Barcode = product.Barcode;
            }

            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}