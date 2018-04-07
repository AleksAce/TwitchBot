using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace TwitchBotCore.Pages
{
    public class IndexModel : PageModel
    {
        private IConfiguration _appConfiguration;

        public IndexModel(IConfiguration configuration)
        {
            _appConfiguration = configuration;
        }
        [BindProperty]
        public string BotUserName { get; set; }
        [BindProperty]
        public string OAuthToken { get; set; }
        [BindProperty]
        public string ChannelName { get; set; }


        public void OnGet()
        {

       

            BotUserName = _appConfiguration["TwitchConfiguration:BotUserName"];
            OAuthToken = _appConfiguration["TwitchConfiguration:OAuthToken"];
            ChannelName = _appConfiguration["TwitchConfiguration:ChannelName"];
        }
        public IActionResult OnPost()
        {
            _appConfiguration["TwitchConfiguration:BotUserName"] = BotUserName;
            _appConfiguration["TwitchConfiguration:OAuthToken"] = OAuthToken;
            _appConfiguration["TwitchConfiguration:ChannelName"] = ChannelName;


            return Redirect("/TwitchBotRunning");
        }

    }
}