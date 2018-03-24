using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchBotInfrastructure;

namespace TwitchBotCore
{
    public class TwitchBotConfiguration : IBotConfiguration
    {
        public string UserName { get; set; } = "LUL";
        public string OAuthToken { get; set; }
        public string Channel { get; set; }
    }
}
