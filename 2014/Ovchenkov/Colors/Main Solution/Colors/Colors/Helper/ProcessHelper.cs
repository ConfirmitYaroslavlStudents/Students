namespace Colors.Helper
{
    public class ProcessHelper
    {
        private ColorKeeper<IColor> _saverOne;
        private ColorKeeper<IColor> _saverTwo;

        public void SetColor(Green color)
        {
            if (_saverOne == null)
            {
                SetFirstColor(color, Colors.Green);
            }
            else
            {
                SetSecondColor(color, Colors.Green);
            }
        }
        public void SetColor(Red color)
        {
            if (_saverOne == null)
            {
                SetFirstColor(color, Colors.Red);
            }
            else
            {
                SetSecondColor(color, Colors.Red);
            }
        }

        private void SetFirstColor(IColor color, Colors typeOfColor)
        {
            _saverOne = new ColorKeeper<IColor> { Value = color, Color = typeOfColor };
        }
        private void SetSecondColor(IColor color, Colors typeOfColor)
        {
            _saverTwo = new ColorKeeper<IColor> { Value = color, Color = typeOfColor };
        }

        public void Process(IProcessor visitor)
        {
            if (_saverOne.Color == Colors.Red && _saverTwo.Color == Colors.Red)
            {
                visitor.Process(GetThisColor<Red>(_saverOne.Value), GetThisColor<Red>(_saverTwo.Value));
            }
            else if (_saverOne.Color == Colors.Green && _saverTwo.Color == Colors.Red)
            {
                visitor.Process(GetThisColor<Green>(_saverOne.Value), GetThisColor<Red>(_saverTwo.Value));
            }
            else if (_saverOne.Color == Colors.Red && _saverTwo.Color == Colors.Green)
            {
                visitor.Process(GetThisColor<Red>(_saverOne.Value), GetThisColor<Green>(_saverTwo.Value));
            }
            else if (_saverOne.Color == Colors.Green && _saverTwo.Color == Colors.Green)
            {
                visitor.Process(GetThisColor<Green>(_saverOne.Value), GetThisColor<Green>(_saverTwo.Value));
            }
        }

        public void ProcessByConstructor(IProcessor visitor)
        {
            if (_saverOne.Color == Colors.Red && _saverTwo.Color == Colors.Red)
            {
                visitor.Process(new Red(_saverOne.Value), new Red(_saverTwo.Value));
            }
            else if (_saverOne.Color == Colors.Green && _saverTwo.Color == Colors.Red)
            {
                visitor.Process(new Green(_saverOne.Value), new Red(_saverTwo.Value));
            }
            else if (_saverOne.Color == Colors.Red && _saverTwo.Color == Colors.Green)
            {
                visitor.Process(new Red(_saverOne.Value), new Green(_saverTwo.Value));
            }
            else if (_saverOne.Color == Colors.Green && _saverTwo.Color == Colors.Green)
            {
                visitor.Process(new Green(_saverOne.Value), new Green(_saverTwo.Value));
            }
        }

        private T1 GetThisColor<T1>(IColor bottle)
        {
            return (T1)bottle; 
        }
    }
}
