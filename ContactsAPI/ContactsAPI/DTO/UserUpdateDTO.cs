using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.DTO
{
    public class UserUpdateDTO : BaseDTO
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username should be between 2 and 50 characters")]
        public string? Username { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 50 characters")]
        public string? Firstname { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 50 characters")]
        public string? Lastname { get; set; }

        [StringLength(100, ErrorMessage = "Username must not exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
    }
}
