
namespace ColorLibrary
{
    public interface IColor
    {
        void DoWith(IColor color);
        Red ToRed();
        Green ToGreen();
    }
   
}
