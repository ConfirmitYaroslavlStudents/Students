using System.Text;

namespace ToDoLibrary
{
    public static class ValidationNumberInCommands
    {
        public static int IntTryParseAndSubtract(string inputIndex)
        {
            if (!int.TryParse(inputIndex, out var result))
                throw new WrongEnteredCommandException("Wrong index.");

            return result-1;
        }

        public static int IntTryParse(string inputIndex)
        {
            if (!int.TryParse(inputIndex, out var result))
                throw new WrongEnteredCommandException("Wrong number.");

            return result;
        }
    }
}
