using Spectre.Console.Rendering;

namespace InputOutputManagers
{
    public interface IConsole
    {
        void WriteLine(string message);
        string ReadLine();
    }

    public interface IConsoleExtended : IConsole
    {
        void RenderTable(IRenderable table);
        void Clear();
        string GetDescription();
        string GetMenuItemName();
    }
}
