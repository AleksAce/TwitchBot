using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace TwitchBotLib.Commands
{
    public interface ICommand
    {
        Task Execute(string SenderUserName, string Message, TwitchClient client);
        string Name { get; set; }
        string Description { get; set; }

    }

}
