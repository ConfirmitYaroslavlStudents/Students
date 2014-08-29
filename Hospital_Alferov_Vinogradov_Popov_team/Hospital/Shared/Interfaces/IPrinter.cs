namespace Shared.Interfaces
{
    public interface IPrinter
    {
        string PathToFile { get; set; }
        void Print(Person person, Analysis analysis, Template template);
    }
}