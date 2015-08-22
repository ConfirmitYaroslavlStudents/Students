using System.Collections.Generic;
using System.Text;

namespace CommandCreation
{
    internal class HelpCommand : Command
    {
        private static readonly Dictionary<string, string> HelpMessages = new Dictionary<string, string>
        {
            {CommandNames.Help, ""},
            {CommandNames.Rename, @"<path> <mask>"},
            {CommandNames.ChangeTags, @"<path> <mask>"},
            {CommandNames.Analyse, @"<path> <mask>"}
        };

        private readonly string _commandForHelp;

        public HelpCommand(string[] args)
        {
            _commandForHelp = args.Length == 2 ? args[1] : null;
        }

        public override string Execute()
        {
            var resultMessage = new StringBuilder();

            if (_commandForHelp == null)
            {
                foreach (var message in HelpMessages)
                {
                    resultMessage.Append(message.Key + ": " + message.Value);
                    resultMessage.Append("\n");
                }
            }
            else
            {
                resultMessage.Append(HelpMessages.ContainsKey(_commandForHelp)
                    ? HelpMessages[_commandForHelp]
                    : "There is no such command!");
            }

            return resultMessage.ToString();
        }


        public override void Complete()
        {
        }

        protected override void SetIfShouldBeCompleted()
        {
            ShouldBeCompleted = false;
        }
    }
}