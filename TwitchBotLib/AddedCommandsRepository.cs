
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchBotLib.Commands;

namespace TwitchBotLib
{
    public class AddedCommandsRepository
    {

        MongoClient client;

        IMongoDatabase _db;
        IMongoCollection<AddedCommand> _commandsColleciton;

        public AddedCommandsRepository()
        {
            client = new MongoClient("mongodb://localhost:27017");
            _db = client.GetDatabase("AddedCommands");
            _commandsColleciton = _db.GetCollection<AddedCommand>("AddedCommands");
        }
        public  Task<List<AddedCommand>> GetAllAsync()
        {
            return _commandsColleciton.Find(_=>true).ToListAsync();
        }
       public Task<AddedCommand> GetCommandAsync(string name)
       {
           return _commandsColleciton.Find(cmd=>cmd.Name == name).FirstOrDefaultAsync();
       }
       public Task AddCommandAsync(AddedCommand command)
       {
            return _commandsColleciton.InsertOneAsync(command);
       }
       public Task RemoveCommandAsync(string name)
       {
           return _commandsColleciton.DeleteOneAsync(cmd => cmd.Name == name);
       }
       public Task EditCommand(string name, AddedCommand command)
       {
           return _commandsColleciton.ReplaceOneAsync(cmd => cmd.Name == name, command);
       }

    }
}
