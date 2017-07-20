using System;

namespace RenamerLibrary
{
    public class ArgumentParser
    {
        public Arguments Parse(string[] args)
        {
            var arguments = new Arguments();

            if (args.Length < 2 || args.Length > 3)
            {
                throw new ArgumentException("Недопустимое число аргументов. Проверьте правильность введённых данных.");
            }

            if (!args[0].EndsWith(".mp3"))
            {
                throw new ArgumentException("Неверная маска поиска файлов. Допускаются только файлы с расширением .mp3.");
            }
            arguments.SearchPattern = args[0];

            int next = 1;
            if (args[next] == "-recursive")
            {
                arguments.IsRecursive = true;
                next++;
            }
            if (args[next] == "-toFileName")
            {
                arguments.Action = "MakeFilename";
            }
            else if (args[next] == "-toTag")
            {
                arguments.Action = "MakeTag";
            }
            else
            {
                throw new ArgumentException("Обнаружены недопустимые аргументы. Проверьте правильность введённых данных.");
            }
            return arguments;
        }
    }
}
