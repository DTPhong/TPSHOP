using System.ComponentModel.DataAnnotations;

namespace TPshop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You must input name.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "You must input username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must input password.")]
        [MinLength(6, ErrorMessage = "Must have at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must input email.")]
        [EmailAddress(ErrorMessage = "This is not email.")]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "You must input phone number.")]
        public string PhoneNumber { get; set; }
    }
}