namespace Shared.Interfaces
{
    public interface IPrinter
    {
        string PathToFile { get; set; }
        void Print(string filledHtmlTemplate);
    }
}
