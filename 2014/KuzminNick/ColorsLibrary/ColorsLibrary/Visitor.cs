namespace ColorsLibrary
{
    public class Visitor
    {
        public string ColorCombination { get; set; }
        public void ChooseTypeOfColor(GreenColor greenColor)
        {
            ColorCombination += "Green";
        }

        public void ChooseTypeOfColor(RedColor redColor)
        {
            ColorCombination += "Red";
        }
    }
}