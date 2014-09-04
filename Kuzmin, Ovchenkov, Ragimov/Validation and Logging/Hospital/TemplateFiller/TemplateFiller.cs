using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using LogService;
using Shared;

namespace TemplateFillerLibrary
{
    public class TemplateFiller
    {
        private const string FieldSubstitition = "field";
        private const string ValueSubstitition = "value";

        private readonly string _pathToHtmlTemplate;
        private readonly Dictionary<string, string> _substitutions;

        public TemplateFiller(string pathToHtmlTemplate)
        {
            _pathToHtmlTemplate = pathToHtmlTemplate;
            _substitutions = new Dictionary<string, string>();
        }

        public string FillTemplate(Person person, Analysis analysis, Template template)
        {
            InitSubstitutions(person);

            StringBuilder templateContent = GetTemplateContent();

            FillTemplateTitle(template, templateContent);
            FillPersonFields(templateContent);
            FillAnalysisFields(analysis, template, templateContent);

            //LOGGING
            Logger.Info("Template was filled");
            return templateContent.ToString();
        }

        private void FillTemplateTitle(Template template, StringBuilder templateContent)
        {
            const string templateTitleSubstitution = "TemplateTitleValue";
            CheckFillPosibility(templateContent, templateTitleSubstitution);
            templateContent.Replace(templateTitleSubstitution, template.Title);
            //LOGGING
            Logger.Info("Template was filled");
        }

        private void FillAnalysisFields(Analysis analysis, Template template, StringBuilder templateContent)
        {
            for (int i = 0; i < analysis.Data.Count; ++i)
            {
                string fieldSubstitution = FieldSubstitition + i;
                string valueSubstitution = ValueSubstitition + i;

                CheckFillPosibility(templateContent, fieldSubstitution, valueSubstitution);

                templateContent.Replace(fieldSubstitution, template.Data[i]);
                templateContent.Replace(valueSubstitution, analysis.Data[i]);
            }
            //LOGGING
            Logger.Info("Fields of analysis were filled");
        }

        private void FillPersonFields(StringBuilder templateContent)
        {
            foreach (var substitution in _substitutions)
            {
                CheckFillPosibility(templateContent, substitution.Key);
                templateContent.Replace(substitution.Key, substitution.Value);
            }
            //LOGGING
            Logger.Info("Person's fields were filled");
        }

        private void CheckFillPosibility(StringBuilder templateContent, params string[] substitutions)
        {
            for (int i = 0; i < substitutions.Length; ++i)
            {
                if (!templateContent.ToString().Contains(substitutions[i]))
                {
                    //LOGGING
                    var ex = new FormatException("Incorrect template format!");
                    Logger.Error("Incorrect template format!",ex);
                    throw ex;
                }
            }
            //LOGGING
            Logger.Info("Ability for filling in template was checked");
        }

        private void InitSubstitutions(Person person)
        {
            PropertyInfo[] personProperties = typeof (Person).GetProperties();

            foreach (PropertyInfo personProperty in personProperties)
            {
                _substitutions[personProperty.Name + "Value"] = personProperty.GetValue(person).ToString();
            }
            //LOGGING
            Logger.Info("Subtitutions were initiated");
        }

        private StringBuilder GetTemplateContent()
        {
            return new StringBuilder(File.ReadAllText(_pathToHtmlTemplate));
        }
    }
}