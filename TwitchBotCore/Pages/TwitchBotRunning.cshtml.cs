using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using TwitchBotLib;

namespace TwitchBotCore.Pages
{
    public class TwitchBotRunningModel : PageModel
    {
        private IConfiguration _appConfiguration;
        private Bot _bot;
        public TwitchBotRunningModel(IConfiguration configuration, Bot bot)
        {
            _bot = bot;
            _appConfiguration = configuration;
        }
        public void OnGet()
        {
            TwitchBotConfiguration botConfiguration = new TwitchBotConfiguration()
            {
                Channel = _appConfiguration["TwitchConfiguration:ChannelName"],
                UserName = _appConfiguration["TwitchConfiguration:BotUserName"],
                OAuthToken = _appConfiguration["TwitchConfiguration:OAuthToken"]
            };
            if (_bot.isConnected == false)
            {
                _bot.isConnected = true;//start from here: Add randomized bot positions
                _bot.Start(botConfiguration);
                
            }
            
      
        }
        public ActionResult OnPost()
        {
            
            _bot.Stop();
            return Redirect("/Index");
        }
    }
}