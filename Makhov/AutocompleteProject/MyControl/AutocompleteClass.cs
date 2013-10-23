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
        private List<string> _dataSource;

        private int _maxResults;
        private int _minQueryLength;
        private int _queryDelay = 100;
        private string _resultFilters;
        private string _resultHighlighter;

        //Maximum number of results to display. A value of 0 or less will allow an unlimited number of results
        [DefaultValue(0)]
        public int maxResults { get { return _maxResults; } set { _maxResults = value; } }

        //Minimum number of characters that must be entered before a query event will be fired.
        //A value of 0 allows empty queries; a negative value will effectively disable all query events
        //and turn AutoComplete off
        [DefaultValue(1)]
        public int minQueryLength { get { return _minQueryLength; } set { _minQueryLength = value; } }

        //Number of milliseconds to wait after user input before triggering a query event.
        //If new input occurs before this delay is over, the previous input event will be ignored
        //and a new delay will begin
        [DefaultValue(100)]
        public int queryDelay { get { return _queryDelay; } set { _queryDelay = value; } }

        //Result filter name, function, or array of filter names and/or functions:
        //charMatch, phraseMatch, phraseMatch, subWordMatch, wordMatch
        [DefaultValue("startsWith")]
        public string resultFilters { get { return _resultFilters; } set { _resultFilters = value; } }

        //Result highlighter name or function:
        //charMatch, phraseMatch, phraseMatch, subWordMatch, wordMatch
        [DefaultValue("startsWith")]
        public string resultHighlighter { get { return _resultHighlighter; } set { _resultHighlighter = value; } }

        //Constructor
        public AutocompleteClass()
        {
            _maxResults = 0;
            _minQueryLength = 1;
            _queryDelay = 100;
            _resultFilters = "startsWith";
            _resultHighlighter = "startsWith";
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
            string scriptKey = "";

            //Load the YUI lib if we haven't already loaded it
            if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "loadYUI"))
            {
                scriptKey = "loadYUI_scriptKey";

                scriptText.Append("<script type=\"text/javascript\" src=\"");
                scriptText.Append("http://yui.yahooapis.com/3.5.1/build/yui/yui-min.js");
                scriptText.Append("\"></script>\n");

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptKey, scriptText.ToString());

                scriptText.Clear();
                scriptKey = "";
            }
            //Load complete

            //Implementing autocomplete
            scriptKey = this.ClientID + "_scriptKey";

            scriptText.Append("<script type=\"text/javascript\">\n");
            scriptText.Append("YUI().use(['autocomplete', 'autocomplete-filters', 'autocomplete-highlighters'], function (Y) {\n\n");

            scriptText.Append("var dataList = []\n");
            foreach(var dataElement in _dataSource)
            {
                scriptText.Append("dataList.push('" + dataElement.ToString() + "');\n");
            }
            
            scriptText.Append("Y.one('#" + this.ClientID + "').plug(Y.Plugin.AutoComplete, {\n");
            scriptText.Append("maxResults: " + this.maxResults.ToString() + ",\n");
            scriptText.Append("minQueryLength: " + this.minQueryLength.ToString() + ",\n");
            scriptText.Append("queryDelay: " + this.queryDelay.ToString() + ",\n");
            scriptText.Append("resultFilters: '" + this.resultFilters + "',\n");
            scriptText.Append("resultHighlighter: '" + this.resultHighlighter + "',\n");
            scriptText.Append("source: dataList\n");
            scriptText.Append("});\n");
            scriptText.Append("})\n");
            scriptText.Append("</script>");
            
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptKey, scriptText.ToString());

            scriptText.Clear();
            scriptKey = "";
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
