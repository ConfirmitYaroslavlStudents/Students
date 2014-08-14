
namespace ColorLibrary
{
    public interface IColor
    {
        string DoWith(IColor color);
        Red ToRed();
        Green ToGreen();
    }
   
}
