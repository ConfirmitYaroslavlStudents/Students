using System.Collections.Generic;

namespace Shared.Interfaces
{
    public interface ITemplateDataAccessLayer
    {
        List<Template> GetTemplates();
        Template GetTemplate(string title);
        void AddTemplate(Template template);
    }
}