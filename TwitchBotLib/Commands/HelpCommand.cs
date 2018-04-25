using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace TwitchBotLib.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name { get; set; } = "help";
        public string Description { get; set; } = "info about specific command";

        public async Task Execute(string SenderUserName, string Message, TwitchClient client)
        {
            client.SendMessage("Type !help to see information about a specific command");
        }
    }
}
