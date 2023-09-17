using System.ComponentModel.DataAnnotations;

namespace CodeAssessment.Models
{
    public class UserDetail
    {
        [Key, Required(ErrorMessage ="Id is a required field.")]
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage ="Maximum character for Name is 100")]
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(13)]
        public string? MobileNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
