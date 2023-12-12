using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.DTO
{
    public class ProjectUpdateDTO : BaseDTO
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 255 characters")]
        public string? Title { get; set; }
        public int UserId { get; set; }

        [StringLength(255, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 255 characters")]
        public string? Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? Deadline { get; set; }
    }
}
