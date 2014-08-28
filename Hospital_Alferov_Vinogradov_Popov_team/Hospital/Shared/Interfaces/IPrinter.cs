namespace Shared.Interfaces
{
    public interface IPrinter
    {
        void Print(Person person, Analysis analysis, Template template);
        string PathToFile { get; set; }
    }
}
