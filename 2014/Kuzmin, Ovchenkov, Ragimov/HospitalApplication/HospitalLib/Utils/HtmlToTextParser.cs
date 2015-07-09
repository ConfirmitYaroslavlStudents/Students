using System.Text;
using HtmlAgilityPack;

namespace HospitalLib.Utils
{
    public class HtmlToTextParser
    {
        public string Parse(string html)
        {
            var root = GetRoot(html);
            var result = new StringBuilder();
            var inputs = root.SelectNodes("//input");
            var brs = root.SelectNodes("//br");
            var divs = root.SelectNodes("//div");

            foreach (var node in root.DescendantNodes())
            {
                if (!node.HasChildNodes)
                {
                    GetNodeText(node, result, inputs);
                }
                if (brs != null && brs.Contains(node) || (divs!= null && divs.Contains(node)))
                {
                    result.AppendLine();
                }
            }

            return result.ToString();
        }

        private void GetNodeText(HtmlNode node, StringBuilder result, HtmlNodeCollection inputs)
        {
            var text = node.InnerText;
            if (!string.IsNullOrEmpty(text))
                result.Append(text.Trim());
            else
            {
                if (inputs.Contains(node))
                {
                    var att = node.Attributes["value"];
                    if (att != null)
                        result.Append(" " + att.Value.Trim());
                }
            }
        }

        private static HtmlNode GetRoot(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var root = doc.DocumentNode;

            return root;
        }
    }
}
