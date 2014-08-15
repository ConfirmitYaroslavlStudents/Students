namespace Colors
{
    public class Red : IColor
    {
        public void Mix(int counter, ColorMixer mixer)
        {
            mixer.Add(this, counter);
        }
    }
}
