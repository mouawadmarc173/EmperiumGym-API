using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using imperiumapp.Core.Entities;
using imperiumapp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imperiumapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MembersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var members = await _context.Members.ToListAsync();
            var coaches = await _context.Coaches.ToListAsync();

            // 🔴 التعديل الثاني: ربط الكوتش يدوياً لنجبر السيرفر يبعت اسمه وما يختفي بعد الريفريش
            foreach (var m in members)
            {
                if (m.CoachId.HasValue && m.CoachId.Value > 0)
                {
                    m.AssignedCoach = coaches.FirstOrDefault(c => c.Id == m.CoachId.Value);
                }
            }

            return Ok(members);
        }

        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(Member newMember)
        {
            newMember.JoinDate = DateTime.Now;
            newMember.IsActive = true;
            newMember.TotalDebt = 0;

            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();

            return Ok(newMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member updatedMember)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound(new { message = "Member not found" });
            }

            // تحديث البيانات الأساسية
            member.FullName = updatedMember.FullName;
            member.IdCard = updatedMember.IdCard;
            member.PhoneNumber = updatedMember.PhoneNumber;
            member.EmergencyContact1 = updatedMember.EmergencyContact1;
            member.EmergencyContact2 = updatedMember.EmergencyContact2;
            member.ExpiryDate = updatedMember.ExpiryDate;
            member.CoachId = updatedMember.CoachId;
            member.MedicalRecord = updatedMember.MedicalRecord;

            // 🔴 التعديل الثالث: إجبار السيرفر على حفظ النقاط عشان ما تصفر وتضيع
            member.LoyaltyPoints = updatedMember.LoyaltyPoints;

            // تحديث الأسعار والمالية 
            member.GymSubscriptionPrice = updatedMember.GymSubscriptionPrice;
            member.CoachPrice = updatedMember.CoachPrice;

            if (updatedMember.JoinDate.Date == DateTime.Now.Date)
            {
                member.JoinDate = updatedMember.JoinDate;
            }

            await _context.SaveChangesAsync();

            return Ok(member);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            try
            {
                var member = await _context.Members.FindAsync(id);
                if (member == null)
                {
                    return NotFound(new { message = "Member not found" });
                }

                var memberAttendance = _context.AttendanceLogs.Where(a => a.MemberId == id);
                _context.AttendanceLogs.RemoveRange(memberAttendance);

                var memberSales = _context.SaleTransactions.Where(s => s.MemberId == id);
                _context.SaleTransactions.RemoveRange(memberSales);

                _context.Members.Remove(member);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Member deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ غير متوقع أثناء الحذف.", error = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}