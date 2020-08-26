using System.ComponentModel.DataAnnotations;

namespace BillSplitter.Models.ViewModels.LoginAndRegister
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        public string GivenName { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        public string Surname { get; set; }
    }
}
