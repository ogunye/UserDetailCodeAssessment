using System.ComponentModel.DataAnnotations;

namespace CodeAssessment.Models
{
    public class UserDetail
    {
        [Key, Required(ErrorMessage ="Id is a required field.")]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is a required field.")]
        [MaxLength(100, ErrorMessage ="Maximum character for Name is 100")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Date of Birth is a required field")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(11)]
        public string? MobileNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
