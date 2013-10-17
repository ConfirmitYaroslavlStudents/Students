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
            List<string> numbers = new List<string>();
            
            numbers.Add("zero");
            numbers.Add("one");
            numbers.Add("two");
            numbers.Add("three");
            numbers.Add("four");
            numbers.Add("five");
            numbers.Add("six");
            numbers.Add("seven");
            numbers.Add("eight");
            numbers.Add("nine");
            numbers.Add("ten");
            numbers.Add("eleven");
            numbers.Add("twelve");
            numbers.Add("thirteen");
            numbers.Add("fourteen");
            numbers.Add("fifteen");
            numbers.Add("sixteen");
            numbers.Add("seventeen");
            numbers.Add("eihteen");
            numbers.Add("nineteen");
            numbers.Add("twenty");

            AutoC.GetSourse(numbers);
        }
    }
}