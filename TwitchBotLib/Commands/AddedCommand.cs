using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace TwitchBotLib.Commands
{
    public class AddedCommand : ICommand
    {
        [BsonId] 
        public ObjectId Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Response { get; set; }

        public string Description { get; set; } = "prints static text ";

        public async Task Execute(string SenderUserName, string Message, TwitchClient client)
        {
            client.SendMessage(this.Response);
        }
    }
}
