using DAL_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace GameButlerRazor.Pages
{
    public class LoginModel : PageModel
    {
        public string username { get; set; }
        public string encryptedPassword;
        public GetRsaKey rsaJson = new GetRsaKey();
        public IActionResult OnGet()
        {
            var steamID = Request.Cookies["steamid"];
            var sessionID = Request.Cookies["sessionid"];
            var apiKey = Request.Cookies["ApiKey"];

            if (steamID != null && sessionID != null && apiKey != null)
            {
                Response.Redirect("main");
            }
            else
            {
                return Page();
            }
            return Page();

        }
        public void originalOnGet()
        { 
        
        }
        public void loginClick()
        {
            var steamLogin = new SteamLogin();
            var data = new NameValueCollection { { "username", username } };
            // First get the RSA key with which we will encrypt our password.
            string response = steamLogin.Fetch("https://steamcommunity.com/login/getrsakey", "POST", data, false);
            rsaJson = JsonConvert.DeserializeObject<GetRsaKey>(response);

        }
        public IActionResult OnPost()
        {
            var steamLogin = new SteamLogin();
            var data = new NameValueCollection { { "username", Request.Form["username"] } };
            // First get the RSA key with which we will encrypt our password.
            string response = steamLogin.Fetch("https://steamcommunity.com/login/getrsakey", "POST", data, false);
            rsaJson = JsonConvert.DeserializeObject<GetRsaKey>(response);

            return RedirectToPage("LoginPassword", new {
                username = Request.Form["username"] ,
                publickey_mod = rsaJson.publickey_mod,
                publickey_exp = rsaJson.publickey_exp,
                timestamp = rsaJson.timestamp,
                success = rsaJson.success});
        }

    }
}
