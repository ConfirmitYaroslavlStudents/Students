using ToDoApp;
using Xunit;

namespace ToDoListTestProject
{
    public class CommandLineModeTests
    {
        [Fact]
        public void GetMenuItemNameIsNotCaseSensitive()
        {
            var cmdParserOne = new CommandLineHandler(new []{"Add"});
            var cmdParserTwo = new CommandLineHandler(new[] {"aDD"});
            
            Assert.Equal("add",cmdParserOne.GetMenuItemName());
            Assert.Equal("add",cmdParserTwo.GetMenuItemName());
        }
    }
}
