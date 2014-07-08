using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace AutocompleteControl
{
    public class AutocompleteClass : TextBox
    {
        const string loadYUI_scriptKey = "loadYUI_scriptKey";

        public enum Filters { charMatch, phraseMatch, startsWith, subWordMatch, wordMatch };

        private List<string> _dataSource;

        //Maximum number of results to display. A value of 0 or less will allow an unlimited number of results
        [DefaultValue(0)]
        public int MaxResults { get; set; }

        //Minimum number of characters that must be entered before a query event will be fired.
        //A value of 0 allows empty queries; a negative value will effectively disable all query events
        //and turn AutoComplete off
        [DefaultValue(1)]
        public int MinQueryLength { get; set; }

        //Number of milliseconds to wait after user input before triggering a query event.
        //If new input occurs before this delay is over, the previous input event will be ignored
        //and a new delay will begin
        [DefaultValue(100)]
        public int QueryDelay { get; set; }

        //Result filter name, function, or array of filter names and/or functions:
        [DefaultValue(Filters.startsWith)]
        public Filters ResultFilters { get; set; }

        //Result highlighter name or function:
        [DefaultValue(Filters.startsWith)]
        public Filters ResultHighlighter { get; set; }

        //Constructor
        public AutocompleteClass()
        {
            MaxResults = 0;
            MinQueryLength = 1;
            QueryDelay = 100;
            ResultFilters = Filters.startsWith;
            ResultHighlighter = Filters.startsWith;
        }

        //Set data source for autocomplete control
        public void SetSource(IEnumerable dataSource)
        {
            _dataSource = new List<string>();

            foreach (var elem in dataSource)
                _dataSource.Add(elem.ToString());
        }
  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            StringBuilder scriptText = new StringBuilder("");

            //Load the YUI lib if we haven't already loaded it
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), loadYUI_scriptKey))
            {
                scriptText.Append("<script type=\"text/javascript\" src=\"");
                scriptText.Append("http://yui.yahooapis.com/3.5.1/build/yui/yui-min.js");
                scriptText.Append("\"></script>\n");

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), loadYUI_scriptKey, scriptText.ToString());

                scriptText.Clear();
            }
            //Load complete

            //Implementing autocomplete
            string scriptKey = ClientID + "_scriptKey";

            scriptText.Append("<script type=\"text/javascript\">\n");

            scriptText.Append("YUI().use(['autocomplete', 'autocomplete-filters', 'autocomplete-highlighters'], function (Y) {\n");
            scriptText.Append("var inputNode = Y.one('#" + ClientID + "');\n");

            scriptText.Append("var dataList = [];\n");
            foreach (var dataElement in _dataSource)
            {
                scriptText.Append("dataList.push('" + dataElement.ToString() + "');\n");
            }

            scriptText.Append("inputNode.plug(Y.Plugin.AutoComplete, {\n");
            scriptText.Append("allowTrailingDelimiter: true,\n");
            scriptText.Append("queryDelimiter: ',',\n");
            scriptText.Append("maxResults: " + MaxResults + ",\n");
            scriptText.Append("minQueryLength: " + MinQueryLength + ",\n");
            scriptText.Append("queryDelay: " + QueryDelay + ",\n");
            scriptText.Append("source: dataList,\n");
            scriptText.Append("resultHighlighter: '" + ResultFilters + "',\n");

            scriptText.Append("resultFilters: ['" + ResultFilters + "', function (query, results) {\n");
            scriptText.Append("var selected = inputNode.get('value').split(/\\s*,\\s*/);\n");
            scriptText.Append("selected = Y.Array.hash(selected);\n");
            scriptText.Append("return Y.Array.filter(results, function (result) {\n");
            scriptText.Append("return !selected.hasOwnProperty(result.text);\n");
            scriptText.Append("});\n");
            scriptText.Append("}]\n");

            scriptText.Append("});\n");
            scriptText.Append("});\n");

            scriptText.Append("</script>");

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptKey, scriptText.ToString());
            //Implementing complete
        }


        protected override void Render(HtmlTextWriter writer)
        {
            //Dynamic adding <div> tag
            writer.Write("<div class=\"yui3-skin-sam\">");

            base.Render(writer);

            writer.Write("</div>");
            //Adding complete
        }
    }
}
