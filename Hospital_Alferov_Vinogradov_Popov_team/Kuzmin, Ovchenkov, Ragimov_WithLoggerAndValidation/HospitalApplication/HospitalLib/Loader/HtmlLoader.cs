using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using HospitalLib.Data;
using HospitalLib.DatebaseModel;
using HospitalLib.Interfaces;

namespace HospitalLib.Loader
{
    public class HtmlLoader : ITemplateLoader
    {
        private const string PathToFolder = @"\Templates\";
        private readonly ITemplateProvider _templateProvider;

        public HtmlLoader(ITemplateProvider templateProvider)
        {
            if (templateProvider == null)
                throw new NullReferenceException("templateProvider");
            _templateProvider = templateProvider;
        }

        public void Load()
        {
            var result = new List<Template>();
            string[] templatesPath = GetTemplatesPath();

            if (templatesPath.Length == 0)
            {
                return;
            }

            foreach (string path in templatesPath)
            {
                result.Add(LoadTemplate(path));
            }

            UpdateDatabase(result, _templateProvider);
        }

        private static string[] GetTemplatesPath()
        {
            string pathToDirectory = Environment.CurrentDirectory + PathToFolder;
            const string fileNameTemplate = "*.html";

            string[] templatesPath = {};
            try
            {
                templatesPath = Directory.GetFiles(pathToDirectory, fileNameTemplate);
            }
            catch (IOException)
            {
                throw new IOException("Что-то не так с папкой для шаблонов!");
            }

            return templatesPath;
        }

        private void UpdateDatabase(IEnumerable<Template> templates, ITemplateProvider templateProvider)
        {
            IList<Template> templatesFromDatabase = templateProvider.Load();

            Dictionary<string, Template> names =
                templatesFromDatabase.ToDictionary(templateFromDatabase => templateFromDatabase.Name);

            foreach (Template loadedTemplate in templates)
            {
                if (names.Keys.Contains(loadedTemplate.Name))
                {
                    Template template = names[loadedTemplate.Name];
                    template.HtmlTemplate = loadedTemplate.HtmlTemplate;
                    templateProvider.Update(template);
                }
                else
                    templateProvider.Save(loadedTemplate);
            }
        }

        private Template LoadTemplate(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            string html;
            const string utf8Charset = "<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>";
            try
            {
                html = File.ReadAllText(path);
                if (html.IndexOf("<meta", StringComparison.Ordinal) == -1)
                {
                    html = utf8Charset + Environment.NewLine + html;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong with template " + name, "Error!");
                return null;
            }

            return new Template(name, html);
        }
    }
}