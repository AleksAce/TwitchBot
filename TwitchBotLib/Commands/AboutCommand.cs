using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace TwitchBotLib.Commands
{
    public class AboutCommand : ICommand
    {
        public string Name { get; set; } = "about";
        public string Description { get; set; } = "Details about channel";

        public async Task Execute(string SenderUserName, string Message, TwitchClient client)
        {
            client.SendMessage("Channel info: ");
        }
    }
}
