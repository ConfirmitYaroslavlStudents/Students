using HospitalLib.Database;

namespace HospitalLib.Interfaces
{
    public interface IPrint
    {
        void Print(Template.Template template, Person person);
    }
}
