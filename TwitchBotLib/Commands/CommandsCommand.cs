using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace TwitchBotLib.Commands
{
    public class CommandsCommand : ICommand
    {

        public string Name { get; set; } = "commands";
        public string Description { get; set; } = "show available commands";


        public async Task Execute(string SenderUserName, string Message, TwitchClient client)
        {
            client.SendMessage("List of available commands: !about !commands");
        }
    }

   
    
}
