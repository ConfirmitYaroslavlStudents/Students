using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
    }
}
