using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutocompleteProject
{
    public partial class MainForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Numbers sourse for MyControl
            List<string> numbers = new List<string>
            {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eihteen", "nineteen", "twenty"
            };
            
            acNumbers.GetSource(numbers);

            List<string> colours = new List<string>
            {
                "Aqua", "Black", "Blue", "Brown", "Gray", "White", "Gold", "Green", "Yellow", "Orange", "Red"
            };

            acColours.GetSource(colours);
        }
    }
}