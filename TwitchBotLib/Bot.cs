using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotInfrastructure;
using TwitchBotLib.Commands;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBotLib
{
    public class Bot
    {
        private AddedCommandsRepository _addedCommandsRepository;
        List<AddedCommand> _addedCommands;
        public Bot()
        {
          
        }

        public BossFightGame bossFightGame;
        TwitchClient client = new TwitchClient();
        public bool isConnected = false;
        IBotConfiguration _botConfiguration;
        
        private Dictionary<string, ICommand> _CommandRegistry = new Dictionary<string, ICommand>();
        
        const char CMD_PREFIX = '!';

        public string Name { get; set; } = "TheBot";
       

        public async Task Start(IBotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
            Name = _botConfiguration.UserName;
            ConnectionCredentials credentials = new ConnectionCredentials(_botConfiguration.UserName, _botConfiguration.OAuthToken);
            bossFightGame = new BossFightGame();
            client = new TwitchClient();
            client.Initialize(credentials, _botConfiguration.Channel);

           // client.OnConnected += onConnected;
           // client.OnJoinedChannel += onJoinedChannel;
            client.OnMessageReceived += onMessageRecieved;
           // client.OnLeftChannel += onLeftChannel;
           // client.OnDisconnected += onDisconnected;
            client.Connect();
            await RegisterCommands();

           
           

        }
        
        public string GetCommand(string chatMessage)
        {
            string Result = null;
            string[] msgs = chatMessage.Substring(1, chatMessage.Length - 1).Split(' ');
            Result = msgs[0];
            return Result;
        }
        public string GetMessageWithoutCommand(string chatMessage)
        {
            string Result = null;
            string Command = GetCommand(chatMessage);
            if ((Command.Length + 1) < chatMessage.Length)
            {
                Result = chatMessage.Substring(Command.Length + 2);
            }

            return Result;
        }
                
        private void onMessageRecieved(object sender, OnMessageReceivedArgs e)
        {
            //NOTE: The twitch API Orders the Emotes automatically if the emotes are the same... Nothing you can do about that...
            //It's an emote
            if (e.ChatMessage.EmoteSet.Emotes != null)
            {
                if (bossFightGame != null && bossFightGame.isRunning)
                {
                    //Copy the emotes imgUrls
                    bossFightGame.emotes = e.ChatMessage.EmoteSet.Emotes.Select(em => em.ImageUrl).ToArray();
                }
            }
            //It's a command
            if (e.ChatMessage.Message != null && e.ChatMessage.Message[0] == CMD_PREFIX)
            {
                ProcessCommands(e);
            }
            else
            {
                if (e.ChatMessage.Message[0] == '!')
                client.SendMessage("That command doesn't exist");
            }
          
            //Parse other messages
            if (e.ChatMessage.Message == "hi aflamebot")
                client.SendWhisper(e.ChatMessage.Username, "Hello there");
        }
        private void onLeftChannel(object sender, OnLeftChannelArgs e)
        {
            client.SendMessage("Leaving Channel " + e.Channel);
        }
        private void onJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            client.SendMessage(" " + e.Channel);
        }
        private void onDisconnected(object sender, OnDisconnectedArgs e)
        {
            Console.WriteLine("Disconected BotName: " + e.BotUsername + " " + DateTime.Now);
        }

        private void onConnected(object sender, OnConnectedArgs e)
        {
            client.SendMessage("Hi " + e.BotUsername + "Connected");
        }
        public void Stop()
        {
            isConnected = false;
            client.Disconnect();
        }
        //Registers commands from the AddedCommands Repo and the "Special" commands registry
        public async Task RegisterCommands()
        {
            _addedCommandsRepository = new AddedCommandsRepository();
            _addedCommands = await _addedCommandsRepository.GetAllAsync();

            //CommandRegistry
            _CommandRegistry.Clear();
            var commandTypes = GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommand)));
            foreach (var type in commandTypes)
            {
                var command = Activator.CreateInstance(type) as ICommand;
                _CommandRegistry.Add(command.Name, command);
            }
        }
        
        //Processes different types of commands
        public void ProcessCommands(OnMessageReceivedArgs e)
        {
            //GetCommand Words
            string TheCommand = GetCommand(e.ChatMessage.Message);
            string MessageWithoutCommand = GetMessageWithoutCommand(e.ChatMessage.Message);



            ICommand command = FindCommand(TheCommand);

            if (command != null)
            {
                //Specific for help command
                if (command is HelpCommand)
                {
                    if (MessageWithoutCommand != null)
                    {
                        string comandToHelpWith = MessageWithoutCommand.Split(' ')[0];
                        if (comandToHelpWith[0] == '!')
                        {
                            ICommand secondCommand = FindCommand(comandToHelpWith);
                            if (secondCommand != null)
                            {
                                client.SendMessage(secondCommand.Name + ": " + secondCommand.Description);
                               
                            }
                            else
                            {
                                client.SendMessage("that command does not exist. Use !commands to see list of avalable commands.");
                            }
                        }


                    }

                }
                //Rest of the commands
                else
                {
                    command.Execute(e.ChatMessage.Username, MessageWithoutCommand, client);
                }   
            }
            //Command does not exist
        }
        public ICommand FindCommand(string theCommand)
        {
            //Note: Check the added command DB first then check the command registry
            ICommand Result = null;
            AddedCommand addedCmd = _addedCommands.FirstOrDefault(c => c.Name == theCommand);
            if(addedCmd != null)
            {
                return addedCmd;
            }
            var testCommand = _CommandRegistry.TryGetValue(theCommand, out Result);
            //return the found command or null
            return Result;
        }
    }
}
