namespace ColorsLibrary
{
    public enum TypesOfColorCombination { GreenRed, RedGreen, GreenGreen, RedRed }
    public class Processor
    {
        public TypesOfColorCombination TypeOfColorCombination;

        public TypesOfColorCombination Process(GreenColor greenColor, RedColor redColor)
        {
            return TypesOfColorCombination.GreenRed;
        }

        public TypesOfColorCombination Process(RedColor redColor, GreenColor greenColor)
        {
            return TypesOfColorCombination.RedGreen;
        }

        public TypesOfColorCombination Process(GreenColor firstGreenColor, GreenColor secondGreenColor)
        {
            return TypesOfColorCombination.GreenGreen;
        }

        public TypesOfColorCombination Process(RedColor firstRedColor, RedColor secondRedColor)
        {
            return TypesOfColorCombination.RedRed;
        }
    }
}
