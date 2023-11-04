using DAL_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace GameButlerRazor.Pages
{
    public class LoginPasswordModel : PageModel
    {
        public string username { get; set; }
        public string publickey_mod { get; set; }
        public string publickey_exp { get; set; }
        public string timestamp { get; set; }
        public string success { get; set; }
        public string password { get; set; }

        public static Dictionary<string, string> loggedin = new Dictionary<string, string>();// =

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var steamLogin = new SteamLogin();

            loggedin = steamLogin.DoLoginEncryptedAlready(Request.Form["username"], Request.Form["password"], Request.Form["timestamp"]);

            AuthKeyModel model = new AuthKeyModel();
            DateTime now = DateTime.Now;
            CookieCollection cookieCollection = new CookieCollection();
            Cookie cookieToAdd = new Cookie(); 
            for (int i = 0; i < loggedin.Count; i++)
            {
                cookieToAdd = new Cookie(loggedin.ElementAt(i).Key, loggedin.ElementAt(i).Value);
                cookieToAdd.Expires = now.AddYears(50);
                //cookieCollection.Add(cookieToAdd);
                HttpContext.Response.Cookies.Append(cookieToAdd.Name, cookieToAdd.Value, new CookieOptions {IsEssential=true});
            }
            cookieToAdd = new Cookie("username", Request.Form["username"]);
            HttpContext.Response.Cookies.Append(cookieToAdd.Name, cookieToAdd.Value, new CookieOptions { IsEssential = true });

            cookieToAdd = new Cookie("password", Request.Form["password"]);
            HttpContext.Response.Cookies.Append(cookieToAdd.Name, cookieToAdd.Value, new CookieOptions { IsEssential = true });

            cookieToAdd = new Cookie("timestamp", Request.Form["timestamp"]);
            HttpContext.Response.Cookies.Append(cookieToAdd.Name, cookieToAdd.Value, new CookieOptions { IsEssential = true });

            if (loggedin["EmailAuthNeeded"] == "true")
            {
                model.EmailAuthNeeded = true;
                return RedirectToPage("AuthKey", new { _emailAuth = true, _twoFactor = false, _challenge = "", _authkey = false });

            }
                if (loggedin["TwoFactorNeeded"] == "true")
            {
                model.TwoFactorNeeded = true;
                return RedirectToPage("AuthKey",new {_emailAuth = false,  _twoFactor = true,  _challenge = "",  _authkey = false } );

            }
            if (loggedin["ChallengeURL"] != "")
            {
                model.ChallengeURL = loggedin["ChallengeURL"];
                return RedirectToPage("AuthKey", new { _emailAuth = false, _twoFactor = false, _challenge = loggedin["ChallengeURL"], _authkey = false });

            }
            var steamController = new SteamController();

            if (Request.Cookies["ApiKey"] == "")
            {
                return RedirectToPage("AuthKey", new { _emailAuth = false, _twoFactor = false, _challenge = "", _authkey = true });

            }
            //steamController.GetAPIKey(loggedin);
            return RedirectToPage("SignalR_Chat");

        }
    }
}
