using System;

namespace WebApplication.DTOs
{
    public class EnrollmentResponse
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int  IdStudy { get; set; }
        public String  StartDate { get; set; }
    }
}