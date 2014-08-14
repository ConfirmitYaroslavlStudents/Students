namespace ColorLibrary
{
    public class Red : IColored
    {
        public string Paint()
        {
            return "red";
        }

        public Red(IColored red)
        {

        }

        public Red()
        {
            
        }
    }
}
