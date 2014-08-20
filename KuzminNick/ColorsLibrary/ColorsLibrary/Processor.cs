namespace ColorsLibrary
{
    public class Processor
    {
        public string Process(IColorable firstColorable, IColorable secondColorable)
        {
            // Фактически исходная задача не решена, т.к. мы лишь определяем во времени выполении тип параметров, 
            // но не выбор одной из полиморфных перегрузок ниже
            var visitor = new Visitor();
            firstColorable.IdentifyItself(visitor);
            secondColorable.IdentifyItself(visitor);
            return visitor.ColorCombination;
        }

        public string Process(GreenColor greenColor, RedColor redColor)
        {
            return "GreenRed";
        }

        public string Process(RedColor redColor, GreenColor greenColor)
        {
            return "RedGreen";
        }

        public string Process(GreenColor firstGreenColor, GreenColor secondGreenColor)
        {
            return "GreenGreen";
        }

        public string Process(RedColor firstRedColor, RedColor secondRedColor)
        {
            return "RedRed";
        }
    }
}
