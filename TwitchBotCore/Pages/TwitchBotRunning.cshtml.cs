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
        public ActionResult OnGet()
        {
            TwitchBotConfiguration botConfiguration = new TwitchBotConfiguration()
            {
                Channel = _appConfiguration["TwitchConfiguration:ChannelName"],
                UserName = _appConfiguration["TwitchConfiguration:BotUserName"],
                OAuthToken = _appConfiguration["TwitchConfiguration:OAuthToken"]
            };
            try
            {


                if (_bot.isConnected == false)
                {
                    _bot.isConnected = true;
                    _bot.Start(botConfiguration).GetAwaiter().GetResult();

                }
            }
            catch
            {
                Console.WriteLine("Could not start bot, check your credentials!");
                return RedirectToPage("Index", new { error = "Could not start bot, check your credentials!" });
            }
            return null;
      
        }
        public ActionResult OnPost()
        {
            
            _bot.Stop();
            return Redirect("/Index");
        }
    }
}