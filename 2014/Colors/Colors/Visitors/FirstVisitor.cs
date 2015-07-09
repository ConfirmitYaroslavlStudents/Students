using System;
using Colors.Colors;

namespace Colors.Visitors
{
    internal class FirstVisitor : IColorVisitor<IColorVisitor<string>>
    {
        private readonly ColorProcessor _colorProcessor;

        public FirstVisitor(ColorProcessor colorProcessor)
        {
            _colorProcessor = colorProcessor;
        }

        public IColorVisitor<string> Visit(Red first)
        {
            var secondVisitor = CreateVisitor(first);
            secondVisitor.TakeRed = second => _colorProcessor.Prosess(first, second);
            secondVisitor.TakeBlue = second => _colorProcessor.Prosess(first, second);

            return secondVisitor;
        }

        public IColorVisitor<string> Visit(Green first)
        {
            var secondVisitor = CreateVisitor(first);
            secondVisitor.TakeGreen = second => _colorProcessor.Prosess(first, second);

            return secondVisitor;
        }

        public IColorVisitor<string> Visit(Blue first)
        {
            var secondVisitor = CreateVisitor(first);
            secondVisitor.TakeGreen = second => _colorProcessor.Prosess(first, second);

            return secondVisitor;
        }

        private SecondVisitor CreateVisitor(IColor first)
        {
            Func<IColor, string> defaultCase = second => _colorProcessor.ProcessDefault(first, second);
            return new SecondVisitor
            {
                TakeBlue = defaultCase,
                TakeGreen = defaultCase,
                TakeRed = defaultCase
            };
        }
    }
}