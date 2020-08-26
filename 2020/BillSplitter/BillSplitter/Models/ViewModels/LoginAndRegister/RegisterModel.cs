using System.ComponentModel.DataAnnotations;

namespace BillSplitter.Models.ViewModels.LoginAndRegister
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string GivenName { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
