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
            //Numbers sourse for AutocompleteControl
            List<string> numbers = new List<string>
            {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eihteen", "nineteen", "twenty"
            };

            acNumbers.SetSource(numbers);

            //Colours sourse for AutocompleteControl
            List<string> colours = new List<string>
            {
                "Aqua", "Azure", "Black", "Blue", "Brown", "Chocolate", "Gold", "Gray", "Green", "Indigo",
                "Lime", "Maroon", "Olive", "Orange", "Pink", "Purple", "Red", "Salmon", "White", "Yellow"
            };

            acColours.SetSource(colours);
        }

        protected void GoToAJAXpage_Click(object sender, EventArgs e)
        {
            Response.Redirect("AJAX.aspx");
        }
    }
}