using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace TwitchBotCore.Pages
{
    public class ConfigureBossFIghtModel : PageModel
    {
        private IConfiguration _appConfiguration;

        public ConfigureBossFIghtModel(IConfiguration configuration)
        {
            _appConfiguration = configuration;
        }
        [BindProperty]
        public IFormFile BossImgUrl { get; set; } 
        [BindProperty]
        public IFormFile CatapultImgUrl { get; set; }
        [BindProperty]
        public int BossHealth { get; set; }
        const string ImagesServerPath = @"wwwroot/images/BossFight";
        public void OnGet()
        {

        }
        public async Task<ActionResult> OnPostAsync()
        {
          //Check if it's valid
            string CatapultImageName = CatapultImgUrl.FileName;
            string BossImageName = BossImgUrl.FileName;
            if (!CatapultImageName.Contains(".png") || !BossImageName.Contains(".png"))
            {
                ViewData["Error"] = "Please submit a valid png";
                return Page();
            }
            try
            {
                //Catapult
                string CatapultDestPath = Path.Combine(ImagesServerPath, "catapult.png");
                using (var catapultStream = new FileStream(CatapultDestPath, FileMode.Create))
                {
                    await CatapultImgUrl.CopyToAsync(catapultStream);
                }
                //Boss
                string BossDestPath = Path.Combine(ImagesServerPath, "boss.png");
                using (var bossStream = new FileStream(BossDestPath, FileMode.Create))
                {
                    await BossImgUrl.CopyToAsync(bossStream);
                }

            }
            catch
            {
                ViewData["Error"] = "Something went wrong. Could not upload files...";
                return Page();
            }

            return RedirectToPage("BossFight", new { bossHealth = BossHealth } );

        }
    }
}