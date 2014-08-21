using System;
using System.Collections.Generic;
using System.IO;
using HospitalLib.Interfaces;
using HospitalLib.Template;

namespace HospitalLib.Parser
{
    public class TxtParser : ITemplateProvider
    {
        public Template.Template Load(string path)
        {
            const string lineString = "<Line";
            const string lineEndString = "</Line>";

            const string widthString = "Width:";
            const string heightString = "Height:";

            var lines = new List<Line>();
            var textTemplate = GetTextTemplate(path);
           
            var pos = 0;

            while (true)
            {
                var start = textTemplate.IndexOf(lineString, pos, StringComparison.Ordinal);
                if (start == -1) break;

                var linecontentpos = textTemplate.IndexOf('>', start) + 1;
                var lineoptions = textTemplate.Substring(start + lineString.Length, linecontentpos - start);
                var width = GetProperty(widthString, lineoptions);
                var height = GetProperty(heightString, lineoptions);

                var lineendingpos = textTemplate.IndexOf(lineEndString, start, StringComparison.Ordinal);

                var content = textTemplate.Substring(linecontentpos, lineendingpos - linecontentpos);

                var line = ParseLine(content);
                line.Width = width;
                line.Height = height;

                lines.Add(line);

                pos = lineendingpos;
            }

            return new Template.Template(lines, Path.GetFileName(path));
        }

        private string GetTextTemplate(string path)
        {
            string result;
            using (var file = new StreamReader(path))
            {
                result = file.ReadToEnd();
                result = result.Replace("\r\n", "");
            }

            return result;
        }

        private int GetProperty(string template, string line)
        {
            if (line.IndexOf(template, StringComparison.Ordinal) == -1) return 0;
            var pos = line.IndexOf(template, StringComparison.Ordinal);
            if (line.IndexOf(';') == -1) throw new InvalidDataException("Указывайте ; после свойства");
            var value = line.Substring(pos + template.Length, line.IndexOf(';', pos) - pos - template.Length);

            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            throw new InvalidDataException("Свойства элемента указаны неправильно");
        }

        private Line ParseLine(string line)
        {
            const string textboxstring = "TextBox";
            const string labelstring = "Label";

            const string widthstring = "Width:";
            const string heightstring = "Height:";

            var pos = 0;

            var parsedLine = new Line();

            while (true)
            {
                var start = line.IndexOf('<', pos);
                var end = line.IndexOf('>', pos);

                if (start == -1 || end == -1) break;

                int endpos;
                var labelindex = line.IndexOf(labelstring, pos, StringComparison.Ordinal);
                var textboxindex = line.IndexOf(textboxstring, pos, StringComparison.Ordinal);

                if (textboxindex != -1 && (textboxindex < labelindex || labelindex == -1))
                {
                    var options = line.Substring(start + textboxstring.Length + 1, end - start - textboxstring.Length);
                    var width = GetProperty(widthstring, options);
                    var height = GetProperty(heightstring, options);

                    endpos = line.IndexOf("</" + textboxstring + ">", pos, StringComparison.Ordinal);

                    if (endpos == -1) throw new InvalidDataException("Незакрытый <TextBox>");

                    var content = line.Substring(end + 1, endpos - end - 1);

                    var textbox = new TextBox(content, height, width);

                    parsedLine.AddElement(textbox);

                    pos = endpos + textboxstring.Length + 3;

                }
                else if (labelindex != -1 && (textboxindex > labelindex || textboxindex == -1))
                {
                    var options = line.Substring(start + labelstring.Length + 1, end - start - labelstring.Length - 1);
                    var width = GetProperty(widthstring, options);
                    var height = GetProperty(heightstring, options);

                    endpos = line.IndexOf("</" + labelstring + ">", pos, StringComparison.Ordinal);

                    if (endpos == -1) throw new InvalidDataException("Незакрытый <Label>");

                    var content = line.Substring(end + 1, endpos - end - 1);

                    var label = new Label(content, height, width);
                    parsedLine.AddElement(label);

                    pos = endpos + labelstring.Length + 3;
                }
                else
                {
                    throw new InvalidDataException("Недопустимый элемент в Line");
                }
            }
            return parsedLine;
        }
    }
}
