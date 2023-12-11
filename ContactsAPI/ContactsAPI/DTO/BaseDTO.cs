using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.DTO
{
    public abstract class BaseDTO
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CreatedAt { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime UpdatedAt { get; set; }
    }
}
