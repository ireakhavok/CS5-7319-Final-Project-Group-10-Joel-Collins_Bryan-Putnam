using DAL_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameButlerRazor.Pages
{
    public class GameTableModel : PageModel
    {

        string steamID;
        string ApiKey;
        public SteamController steamController = new SteamController();

        public IActionResult OnGet()
        {
            steamID = Request.Cookies["steamid"];
            ApiKey = Request.Cookies["ApiKey"];
            LoadData();
            return Page();
        }
        private async Task LoadData()
        {
            try
            {
                steamController.apiKey = ApiKey;
                steamController.GetGamesNonAsync(steamID);
                steamController.getInfoForGamesNonAsync();
                steamController.getPlayerCountForGamesNonAsync();
                steamController.getNewsDatesForGamesNonAsync();

            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
