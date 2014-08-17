using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatementArchitecture;

namespace StatementTests
{
    [TestClass]
    public class StatementTests
    {
        StringStatement stringStatement = new StringStatement();
        JsonStatement jsonStatement = new JsonStatement();
        HtmlStatement htmlStatement = new HtmlStatement();
        ParanoicStatement paranoicStatement = new ParanoicStatement();
        ConsoleStatement consoleStatement = new ConsoleStatement();
        [TestMethod]
        public void SingleStatement_ShouldPass()
        {
            var statement = new Statement(stringStatement);
            statement.Generate();

            Assert.AreEqual("This is statement", Receiver.Received);
            Receiver.Clear();
        }

        public void TwoStatements_ShouldPass()
        {
            var statement = new Statement(stringStatement + htmlStatement);
            statement.Generate();

            Assert.AreEqual("This is statement" + "<html>This is statement</html>", Receiver.Received);
            Receiver.Clear();
        }

        public void TripleStatement_ShouldPass()
        {

            var triplestatement = new Statement(htmlStatement + paranoicStatement + consoleStatement);
            triplestatement.Generate();
            Assert.AreEqual("<html>This is statement</html>", Receiver.Received);
            Receiver.Clear();
        }

        public void AllStatementsAtOnce_ShouldPass()
        {
            var allstatements = new Statement(htmlStatement + jsonStatement + paranoicStatement + stringStatement + consoleStatement);
            allstatements.Generate();

            Assert.AreEqual("<html>This is statement</html>" + new MemoryStream() + "This is statement", Receiver.Received);
            Receiver.Clear();
        }
    }
}
