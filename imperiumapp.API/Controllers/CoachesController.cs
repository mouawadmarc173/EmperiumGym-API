using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace imperiumapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private static List<CoachDto> _coaches = new List<CoachDto>();

        // 1. جلب الكباتن (GET)
        [HttpGet]
        public IActionResult GetCoaches()
        {
            return Ok(_coaches);
        }

        // 2. إضافة كابتن (POST)
        [HttpPost]
        public IActionResult AddCoach([FromBody] CoachDto newCoach)
        {
            newCoach.Id = _coaches.Any() ? _coaches.Max(c => c.Id) + 1 : 1;
            _coaches.Add(newCoach);
            return Ok(newCoach);
        }

        // 3. تعديل كابتن (PUT) - هيدي الدالة الجديدة اللي طلبناها!
        [HttpPut("{id}")]
        public IActionResult UpdateCoach(int id, [FromBody] CoachDto updatedCoach)
        {
            var coach = _coaches.FirstOrDefault(c => c.Id == id);
            if (coach == null)
            {
                return NotFound(new { message = "Coach not found" });
            }

            // تحديث المعلومات
            coach.Name = updatedCoach.Name;
            coach.RoleOrSpecialty = updatedCoach.RoleOrSpecialty;
            coach.Notes = updatedCoach.Notes;

            return Ok(coach);
        }

        // 4. حذف كابتن (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteCoach(int id)
        {
            var coach = _coaches.FirstOrDefault(c => c.Id == id);
            if (coach != null)
            {
                _coaches.Remove(coach);
                return Ok(new { message = "Coach deleted successfully" });
            }
            return NotFound(new { message = "Coach not found" });
        }
    }

    // ضفنا الـ Notes على الـ Model
    public class CoachDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RoleOrSpecialty { get; set; }
        public string Notes { get; set; }
    }
}