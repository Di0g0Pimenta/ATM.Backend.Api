using System.ComponentModel.DataAnnotations;

namespace ATM.Backend.Api.Dto
{
    public class UpdatePasswordDto
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string NewPassword { get; set; }
    }
}
