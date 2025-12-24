using System;

namespace LAB_3.Models
{
    public class Postgraduate
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string StudyForm { get; set; } = "Очна";
        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}
