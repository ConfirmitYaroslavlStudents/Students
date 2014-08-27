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
        private readonly INewIdProvider _newIdProvider;
        private readonly ITemplateProvider _templateProvider;

        public HtmlLoader(ITemplateProvider templateProvider, INewIdProvider newIdProvider)
        {
            if (templateProvider == null)
                throw new NullReferenceException("templateProvider");
            _templateProvider = templateProvider;

            if (newIdProvider == null)
                throw new NullReferenceException("NewIdProvider");
            _newIdProvider = newIdProvider;
        }

        public void Load()
        {
            var pathToDirectory = Environment.CurrentDirectory + PathToFolder;

            const string fileNameTemplate = "*.html";
            var result = new List<Template>();

            var templatePath = Directory.GetFiles(pathToDirectory, fileNameTemplate);

            if (templatePath.Length == 0)
            {
                MessageBox.Show("Something wrong with template folder!", "Error!");
                return;
            }

            foreach (var path in templatePath)
            {
                result.Add(LoadTemplate(path, _newIdProvider));
            }

            UpdateDatabase(result, _templateProvider);
        }

        private void UpdateDatabase(IEnumerable<Template> templates, ITemplateProvider templateProvider)
        {
            var templatesFromDatabase = templateProvider.Load();

            var names = templatesFromDatabase.ToDictionary(templateFromDatabase => templateFromDatabase.Name);

            foreach (var loadedTemplate in templates)
            {
                if (templatesFromDatabase.Count == 0)
                    templateProvider.Save(loadedTemplate);

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

        private Template LoadTemplate(string path, INewIdProvider newIdProvider)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            var html = File.ReadAllText(path);

            return new Template(name, html, newIdProvider);
        }
    }
}
