using System.Windows.Controls;

namespace HospitalLib.Interfaces
{
    public interface IElement
    {
        int Width { get; }
        int Height { get; }
        string Text { get; }
        string Type { get; }
        Control GetControl();
    }
}
