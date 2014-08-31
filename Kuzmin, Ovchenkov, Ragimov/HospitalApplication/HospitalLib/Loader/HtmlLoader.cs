using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var templatesPath = GetTemplatesPath();

            if (templatesPath.Length == 0)
            {
                return;
            }

            foreach (var path in templatesPath)
            {
                result.Add(LoadTemplate(path));
            }

            UpdateDatabase(result, _templateProvider);
        }

        private static string[] GetTemplatesPath()
        {
            var pathToDirectory = Environment.CurrentDirectory + PathToFolder;
            const string fileNameTemplate = "*.html";

            string[] templatesPath;
            try
            {
                templatesPath = Directory.GetFiles(pathToDirectory, fileNameTemplate);
            }
            catch (IOException e)
            {
                throw new IOException("Что-то не так с папкой для шаблонов! " + e.Message);
            }      

            return templatesPath;
        }

        private void UpdateDatabase(IEnumerable<Template> templates, ITemplateProvider templateProvider)
        {
            var templatesFromDatabase = templateProvider.Load();

            var names = templatesFromDatabase.ToDictionary(templateFromDatabase => templateFromDatabase.Name);

            foreach (var loadedTemplate in templates)
            {
              if (names.Keys.Contains(loadedTemplate.Name))
                {
                    var template = names[loadedTemplate.Name];
                    template.HtmlTemplate= loadedTemplate.HtmlTemplate;
                    templateProvider.Update(template);
                }
                else
                    templateProvider.Save(loadedTemplate);
            }
        }

        private Template LoadTemplate(string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            string html;
            const string utf8Charset = "<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>";
            try
            {
                html = utf8Charset + Environment.NewLine + File.ReadAllText(path);
            }
            catch (IOException e)
            {
                throw new IOException("Что-то не так с шаблоном! " + e.Message);
            }   

            return new Template(name, html);
        }
    }
}
