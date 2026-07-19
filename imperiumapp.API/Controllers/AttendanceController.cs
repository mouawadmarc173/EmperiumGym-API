using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using imperiumapp.Infrastructure.Data;
using imperiumapp.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace imperiumapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AttendanceController(AppDbContext context) { _context = context; }

        [HttpPost]
        public async Task<IActionResult> LogScan([FromBody] AttendanceLog log)
        {
            // 1. نضمن إنو الوقت هو لحظة الإسكان بالضبط
            log.ScanTime = DateTime.Now;

            // 2. نحفظ الحضور بقاعدة البيانات
            _context.AttendanceLogs.Add(log);
            await _context.SaveChangesAsync();

            // 3. نجيب بيانات العضو اللي عمل إسكان عشان نرجعها للواجهة فوراً (هيدا اللي بيسرّع الظهور)
            var member = await _context.Members.FindAsync(log.MemberId);

            return Ok(new
            {
                Id = log.Id,
                MemberName = member != null ? member.FullName : "عضو غير معروف",
                IdCard = member != null ? member.IdCard : "N/A",
                ScanTime = log.ScanTime
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            // ضفنا حماية عشان إذا العضو انمسح، السيستم ما يضرب ويعطي Error
            var logs = await _context.AttendanceLogs
                .Include(a => a.Member)
                .OrderByDescending(a => a.ScanTime)
                .Select(a => new {
                    Id = a.Id,
                    MemberName = a.Member != null ? a.Member.FullName : "عضو محذوف",
                    IdCard = a.Member != null ? a.Member.IdCard : "N/A",
                    ScanTime = a.ScanTime
                }).ToListAsync();

            return Ok(logs);
        }
    }
}