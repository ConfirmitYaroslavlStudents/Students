using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class ServerControl1 : TextBox
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        /*public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? "[" + this.ID + "]" : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }*/

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            String scriptText1 = "YUI().use(['autocomplete', 'autocomplete-filters', 'autocomplete-highlighters'], function (Y) {\n";
            scriptText1 += "Y.one('#MainContent_AutoC').plug(Y.Plugin.AutoComplete, {\n";
            scriptText1 += "resultFilters: 'startsWith',\n";
            scriptText1 += "resultHighlighter: 'startsWith',\n";
            scriptText1 += "source: ['zero', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine', 'ten', ";
            scriptText1 += "'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen', 'twenty']\n";
            scriptText1 += "});\n";
            scriptText1 += "})";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ConfirmSubmit", scriptText1, true);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }
    }
}
