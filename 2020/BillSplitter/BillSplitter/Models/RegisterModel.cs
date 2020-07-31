using System.ComponentModel.DataAnnotations;

namespace BillSplitter.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
    }
}
