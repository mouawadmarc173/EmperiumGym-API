using System.Collections.Generic;
using System.Text.Json.Serialization; // 👈 ضفنا هيدي المكتبة عشان نستخدم كلمة التجاهل

namespace imperiumapp.Core.Entities
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RoleOrSpecialty { get; set; } = string.Empty;

        // 🔴 هيدي التعويذة السحرية بتمنع الدوران اللانهائي وبتخلي الداتا تظهر فوراً!
        [JsonIgnore]
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}