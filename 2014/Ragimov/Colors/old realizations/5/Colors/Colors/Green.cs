namespace Colors
{
    public class Green : IColor
    {
        public void Mix(int counter, ColorMixer mixer)
        {
            mixer.Add(this,counter);
        }
    }
}
