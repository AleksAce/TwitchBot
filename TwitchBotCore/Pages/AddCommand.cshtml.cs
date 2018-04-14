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
    public class AddCommandModel : PageModel
    {
        private AddedCommandsRepository _addedCommandsRepository;
        [BindProperty]
        public AddedCommand CommandToAdd { get; set; }
        public AddCommandModel(AddedCommandsRepository addedCommandsRepository)
        {
            _addedCommandsRepository = addedCommandsRepository;
        }
        [HttpGet]
        public ActionResult OnGet()
        {
            return Page();
        }
        [HttpPost]
        public async Task<ActionResult> OnpostAsync()
        {
            AddedCommand addedCommand = await _addedCommandsRepository.GetCommandAsync(CommandToAdd.Name);
            if(addedCommand != null)
            {
                ModelState.AddModelError("Command Existing", "Command already exists");
                return Page();
            }
            if(CommandToAdd.Response == null)
            {
                ModelState.AddModelError("Add a Response", "Please add a response");
                return Page();
            }
            try
            {
                await _addedCommandsRepository.AddCommandAsync(CommandToAdd);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Could not add command", "Could not add command, Something went wrong Exception:" + ex.Message);
                return Page();
            }
            
            return RedirectToPage("Commands");
        }
    }
}