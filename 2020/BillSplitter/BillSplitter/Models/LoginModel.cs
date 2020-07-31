using System.ComponentModel.DataAnnotations;

namespace BillSplitter.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
    }
}
