using System;
using System.IO;


namespace StatementArchitecture
{
    public class StringStatement : MixableStatement
    {
        public override void Generate()
        {
            //Let's pretend this is statement
            var stringStatement = "This is statement";
            new Operations<string>().SendToConfirmit(stringStatement);
        }
    }

    public class JsonStatement : MixableStatement
    {
        public override void Generate()
        {
            var jsonStatement = new MemoryStream();
            new Operations<MemoryStream>().SendToConfirmit(jsonStatement);
        }
    }

    public class HtmlStatement : MixableStatement
    {
        public override void Generate()
        {
            var htmlStatement = @"<html>This is statement</html>";
            new Operations<string>().SendToConfirmit(htmlStatement);
        }
    }

    public class ParanoicStatement : MixableStatement
    {
        public override void Generate()
        {
            var paranoicStatement = "This is supersecret statement";
            //Thus statement is super paranoid and it will not send supersecret information to anyone expect face-to-face contact
        }
    }

    public class ConsoleStatement : MixableStatement
    {
        public override void Generate()
        {
            var consoleStatement = "This is looks like a console statement";
            Console.WriteLine(consoleStatement);
        }
    }
}
