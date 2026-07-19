using imperiumapp.Core.Entities;
using imperiumapp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace imperiumapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleTransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SaleTransactionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleTransaction>>> GetSaleTransactions()
        {
            return await _context.SaleTransactions.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<SaleTransaction>> AddSaleTransaction(SaleTransaction saleTransaction)
        {
            _context.SaleTransactions.Add(saleTransaction);

            if (saleTransaction.MemberId != null && saleTransaction.MemberId > 0)
            {
                var member = await _context.Members.FindAsync(saleTransaction.MemberId);

                if (member != null)
                {
                    // 🔴 التعديل الأول: ضربنا المبلغ بـ 10 (كل دولار = 10 نقاط)
                    member.LoyaltyPoints += Convert.ToInt32(saleTransaction.TotalAmount) * 10;
                    _context.Members.Update(member);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(saleTransaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleTransaction(int id)
        {
            var transaction = await _context.SaleTransactions.FindAsync(id);
            if (transaction == null) return NotFound();

            _context.SaleTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearAllTransactions()
        {
            var allTransactions = await _context.SaleTransactions.ToListAsync();
            _context.SaleTransactions.RemoveRange(allTransactions);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}