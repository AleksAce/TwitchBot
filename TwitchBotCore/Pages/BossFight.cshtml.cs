using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace TwitchBotCore.Pages
{
    public class BossFightModel : PageModel
    {
      
        private IConfiguration _appConfiguration;

        public BossFightModel(IConfiguration configuration)
        {
            _appConfiguration = configuration;
        }
        public string BossImage { get; set; }
        public string CatapultImage { get; set; }
        public string BossHealth { get; set; }

        //Todo: Set these manually
        public int BossWidth { get; set; } = 400;
        public int BossHeight { get; set; } = 400;
        public int CatapultWidth { get; set; } = 250;
        public int CatapultHeight { get; set; } = 250;
        public int BulletWidth { get; set; } = 250;
        public int BulletHeight { get; set; } = 250;
      

        public void OnGet(int bossHealth)
        {
           
            if (bossHealth == 0)
            { 
            BossHealth = _appConfiguration["BossFightConfiguration:BossHealth"];
            }
            else
            {
                BossHealth = null;
            }
            BossImage ="/images/BossFight/" + _appConfiguration["BossFightConfiguration:BossImage"];
            CatapultImage = "/images/BossFight/"+ _appConfiguration["BossFightConfiguration:CatapultImage"];
        }
    }
}