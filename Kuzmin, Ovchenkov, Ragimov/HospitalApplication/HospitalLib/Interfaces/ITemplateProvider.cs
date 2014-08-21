namespace HospitalLib.Interfaces
{
    public interface ITemplateProvider
    {
        Template.Template Load(string path);
    }
}
