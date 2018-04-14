using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwitchBotLib;
using TwitchBotLib.Commands;

namespace TwitchBotCore.Pages
{
    public class CommandsModel : PageModel
    {
        private AddedCommandsRepository _addedCommandsRepo;
        public List<AddedCommand> cmds { get; set; }
       
        
        public CommandsModel(AddedCommandsRepository addedCommandsRepo)
        {
            _addedCommandsRepo = addedCommandsRepo;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            try
            {
                //await repo.AddCommand(cmd1);
                cmds = await _addedCommandsRepo.GetAllAsync();
                return Page();

            }
            catch (Exception ex)
            {
                Console.WriteLine("error" + ex.Message);
               
            }
            // Console.WriteLine(await repo.GetCommandAsync(1.ToString()));
            return RedirectToPage("Error");
        }
        [HttpPost]
        public async Task<ActionResult> OnPostAsync(string cmdName)
        {
            if(cmdName != null)
            {
               await _addedCommandsRepo.RemoveCommandAsync(cmdName);
            }
            return RedirectToPage("Commands");
        }
        
    }
}