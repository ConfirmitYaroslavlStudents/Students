using System;

namespace ParseInputLib
{
    public class ParseInput
    {
        public struct InputData
        {
            public string Path { get; set; }
            public string Mask { get; set; }
            public string Modifier { get; set; }
            public bool Subfolders { get; set; }
        }

        public static InputData Parse(string input)
        {
            var item = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (item.Length > 4 || item.Length <3 )
                    throw new ArgumentException("Wrong Input");
                if (item[2] != "-n" && item[2] != "-t")
                    throw new ArgumentException("Wrong Input");
                if (item.Length == 4 && item[3] != "-r")
                    throw new ArgumentException("Wrong Input");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            var inputData = new InputData();
            inputData.Path = item[0];
            inputData.Mask = item[1];
            inputData.Modifier = item[2];
            if (item.Length == 4)
                inputData.Subfolders = true;
            else
                inputData.Subfolders = false;

            return inputData;
        }
    }
}
