using System.ComponentModel.DataAnnotations;

namespace BillSplitter.Models.ViewModels.LoginAndRegister
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
