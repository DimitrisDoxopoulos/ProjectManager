using ContactsAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ContactsAPI.DTO
{
    public class ProjectDTO: BaseDTO
    {
        public int UserId { get; set; }

        public int EmployeeId { get; set; }

        [StringLength(255, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 255 characters")]
        public string? Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Deadline { get; set; }
    }
}
