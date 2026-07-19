namespace imperiumapp.Core.Entities
{
    public class Member
    {
        // 1. البيانات الشخصية والتعريفية
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string IdCard { get; set; } = string.Empty; // كارت الأيدي / الباركود
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmergencyContact1 { get; set; } = string.Empty;
        public string EmergencyContact2 { get; set; } = string.Empty;

        // 2. التواريخ والاشتراكات
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } // تاريخ الانتهاء اللي السيستم رح يشيك عليه للواتساب
        public int LoyaltyPoints { get; set; }
        // 3. الأسعار والمالية
        public decimal GymSubscriptionPrice { get; set; } // سعر اشتراك الجيم
        public decimal CoachPrice { get; set; } // سعر اشتراك المدرب إن وُجد
        public decimal TotalDebt { get; set; } = 0; // إجمالي الديون المسجلة عليه من المحل (Debt)
        public bool IsActive { get; set; } = true;

        // 4. الملف الطبي (Health & Medical Record)
        public string MedicalRecord { get; set; } = string.Empty; // ملاحظات صحية، إصابات، أمراض

        // 5. ربط المشترك بالمدرب (Relationship)
        public int? CoachId { get; set; } // علامة الاستفهام تعني إنه اختياري (ممكن يشترك بدون مدرب)
        public Coach? AssignedCoach { get; set; }
    }
}