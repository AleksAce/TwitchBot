using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TwitchBotInfrastructure;
using TwitchBotLib;

namespace TwitchBotCore
{
    public class Program
    {
        private readonly IBotConfiguration _botConfiguration;
        public Program(IBotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
        }
        public static void Main(string[] args)
        {
            
            BuildWebHost(args).Run();
            
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
