using System.Text;
using HtmlAgilityPack;

namespace HospitalLib.Utils
{
    public class HtmlToTextParser
    {
        public string Parse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var root = doc.DocumentNode;
            var sb = new StringBuilder();
            var inputs = root.SelectNodes("//input");
            foreach (var node in root.DescendantNodes())
            {
                if (!node.HasChildNodes)
                {
                    string text = node.InnerText;
                    if (!string.IsNullOrEmpty(text))
                        sb.AppendLine(text.Trim());
                    else
                    {
                        if(inputs.Contains(node))
                        {
                            var att = node.Attributes["value"];
                            if (att != null)
                                sb.AppendLine(att.Value.Trim());
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
