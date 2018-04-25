using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitchBotLib;

namespace BotTest.cs
{
    [TestClass]
    public class BotLibEventsTest
    {
        [TestMethod]
        public void GetCommand_GivenCommandWordAndAMessage_ReturnsTheCommandOnly()
        {
            Bot bot = new Bot();
            string msg = "!help heasehjashja";
            string result = bot.GetCommand(msg);

            Assert.AreEqual("help", result);
            Assert.IsTrue(result.Length == 4);
        }
        [TestMethod]
        public void GetMessageWithoutCommand_GivenCommandWithMessage_ReturnsTheMessageWithoutCommand()
        {
            Bot bot = new Bot();
            string msg = "!help heasehjashja";
            string command = bot.GetCommand(msg);
            string result = bot.GetMessageWithoutCommand(msg);
            
            int commandPrefix = 1;
            int whiteSpaceAfterCommand = 1; 
            var expectedLenght = msg.Length - (command.Length+ commandPrefix+whiteSpaceAfterCommand );
            Assert.IsTrue(result.Length == expectedLenght );
        }
    }
}
