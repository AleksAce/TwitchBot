using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchBotInfrastructure
{
    public interface IBotConfiguration
    {
        string UserName { get; set; }
        string OAuthToken { get; set; }
        string Channel { get; set; }
    }
}
