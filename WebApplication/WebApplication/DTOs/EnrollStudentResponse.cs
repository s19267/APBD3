using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class EnrollStudentResponse
    {
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String BirthDate { get; set; }
        [Required]
        public string Studies { get; set; }
    }
}
