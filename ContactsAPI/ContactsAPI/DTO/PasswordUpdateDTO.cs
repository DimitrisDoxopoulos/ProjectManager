using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.DTO
{
    public class PasswordUpdateDTO : BaseDTO
    {
        public int UserId { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$",
            ErrorMessage = "The password must contain at least one uppercase letter, " +
            "one lowercase letter, one digit and one special character.")]
        public string? NewPassword { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,}$",
            ErrorMessage = "The password must contain at least one uppercase letter, " +
            "one lowercase letter, one digit and one special character.")]
        public string? NewPasswordConfirm { get; set; }
    }
}
