using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3Lib;

namespace CommandCreation
{
    class AnalyseCommand : Command
    {
        private List<string> _resultMessages;
        private ISource _source;
        private string _mask;


        public AnalyseCommand(ISource source, string mask)
        {
            _source = source;
            _mask = mask;
        }


        public static int[] GetNumberOfArguments()
        {
            return new[] { 3 };
        }

        public override string GetCommandName()
        {
            return CommandNames.Analyse;
        }

        public override void Execute()
        {
            foreach (var file in _source.GetFiles())
            {
                Mp3Manipulations manipulation = new Mp3Manipulations(file);
                _resultMessages.Add(manipulation.Analyse(_mask));
                //show on console _resultMessages
            }
        }
    }
}
