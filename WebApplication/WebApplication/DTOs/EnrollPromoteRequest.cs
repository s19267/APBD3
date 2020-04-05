using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTOs
{
    public class EnrollPromoteRequest
    {
        [Required]
        public string studies { get; set; }
        [Required]
        public int semester { get; set; }
    }
}