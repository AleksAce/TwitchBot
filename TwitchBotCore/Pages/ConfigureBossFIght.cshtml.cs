﻿using System;
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
            try 
            { 
            string CatapultImageName = Path.Combine(ImagesServerPath, "catapult.png");
            if (CatapultImgUrl != null)
            {
                CatapultImageName = CatapultImgUrl.FileName;
                if(!CatapultImageName.Contains(".png"))
                {
                    ViewData["Error"] = "Please submit a valid catapult png";
                    return Page();
                }
                    //Catapult
                string CatapultDestPath = CatapultImageName;
                using (var catapultStream = new FileStream(CatapultDestPath, FileMode.Create))
                {
                    await CatapultImgUrl.CopyToAsync(catapultStream);
                }
            }

            string BossImageName = Path.Combine(ImagesServerPath, "boss.png");
            if (BossImgUrl != null)
            {
                BossImageName = BossImgUrl.FileName;
                if(!BossImageName.Contains(".png"))
                {
                        ViewData["Error"] = "Please submit a valid boss png";
                        return Page();
                }
                string BossDestPath = BossImageName;
                using (var bossStream = new FileStream(BossDestPath, FileMode.Create))
                {
                    await BossImgUrl.CopyToAsync(bossStream);
                }

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