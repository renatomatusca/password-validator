using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class ValidatePasswordRequest
    {
        [Required]
        public string Password { get; set; }
    }
}