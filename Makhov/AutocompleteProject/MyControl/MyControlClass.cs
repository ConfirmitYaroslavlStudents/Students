using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class MyControlClass : TextBox
    {
        private List<string> sourse;

        public void GetSourse(List<string> inputList)
        {
            this.sourse = inputList;
        }
  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            String scriptText = "";

            //Load the YUI lib if we haven't already loaded it
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "scriptKey"))
            {
                scriptText += "<script type=\"text/javascript\" src=\"";
                scriptText += "http://yui.yahooapis.com/3.5.1/build/yui/yui-min.js";
                scriptText += "\"></script>\n\n";
            }

            scriptText += "<script>\n";
            scriptText += "YUI().use(['autocomplete', 'autocomplete-filters', 'autocomplete-highlighters'], function (Y) {\n\n";
            
            scriptText += "var Array = []\n";
            foreach(string str in sourse)
            {
                scriptText += "Array.push('" + str + "');\n";
            }
            
            scriptText += "Y.one('#" + this.ClientID + "').plug(Y.Plugin.AutoComplete, {\n";
            scriptText += "resultFilters: 'startsWith',\n";
            scriptText += "resultHighlighter: 'startsWith',\n";
            scriptText += "source: Array\n";
            scriptText += "});\n";
            scriptText += "})\n";
            scriptText += "</script>";
            
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "scriptKey", scriptText);
        }
    }
}
