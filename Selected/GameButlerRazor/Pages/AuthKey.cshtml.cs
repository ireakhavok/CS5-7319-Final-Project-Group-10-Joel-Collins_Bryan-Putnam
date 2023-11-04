using DAL_API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Specialized;
using System.Net;

namespace GameButlerRazor.Pages
{
    public class AuthKeyModel : PageModel
    {
        public string response {  get; set; }
        public bool EmailAuthNeeded { get; set; }
        public bool TwoFactorNeeded { get; set; }
        public string? ChallengeURL { get; set; }
        public bool authKeyneeded { get; set; }
        public void OnGet(bool _emailAuth, bool _twoFactor, string _challenge, bool _authkey)
        {
            this.EmailAuthNeeded = _emailAuth;
            this.TwoFactorNeeded = _twoFactor;
            this.ChallengeURL = _challenge;
            this.authKeyneeded = _authkey;
        }

        //public void ModelStart(bool _emailAuth, bool _twoFactor, string _challenge, bool _authkey)
        //{
        //    this.EmailAuthNeeded = _emailAuth;
        //    this.TwoFactorNeeded = _twoFactor;
        //    this.ChallengeURL = _challenge;
        //    this.authKeyneeded = _authkey;
        //}

        public IActionResult OnPost()
        {
            var responseString = Request.Form["response"];
            var steamID = Request.Cookies["steamid"];
            if ( Request.Form["authKeyneeded"] == "True")
            {
                Cookie cookieToAdd = new Cookie("ApiKey", responseString);
                HttpContext.Response.Cookies.Append(cookieToAdd.Name, cookieToAdd.Value, new CookieOptions { IsEssential = true });
                return RedirectToPage("main");
            }
            var loginController = new SteamLogin();
            bool emailAuthBool = false; 
            bool twoFactorBool = false;
            if (Request.Cookies["EmailAuthNeeded"] == "true")
            {
                emailAuthBool = true;
            }
            else if (Request.Cookies["TwoFactorNeeded"] == "true")
            {
                twoFactorBool = true;
            }
            else if (Request.Cookies["ApiKey"] != "")
            {
                return RedirectToPage("main");
            }
            Dictionary<String, String> result = loginController.DoLoginEncryptedAlready(Request.Cookies["username"], Request.Cookies["password"], Request.Cookies["timestamp"], Request.Form["response"],
               emailAuthBool, twoFactorBool);

            string sessionValue = "";
            Dictionary<string, string> cookieDictionary = new Dictionary<string, string>();
            for (int i = 0; i < Request.Cookies.Count(); i++)
            {
                cookieDictionary.Add(Request.Cookies.ElementAt(i).Key, Request.Cookies.ElementAt(i).Value);
            }

            bool gotSession = result.TryGetValue("sessionid", out sessionValue);
            if (gotSession)
            {
                if (sessionValue != "")
                {
                    Cookie cookieToAdd = new Cookie("sessionid", result["sessionid"]);
                    HttpContext.Response.Cookies.Append(cookieToAdd.Name, cookieToAdd.Value, new CookieOptions { IsEssential = true });
                    bool wasAdded = cookieDictionary.TryAdd("sessionid", result["sessionid"]);
                    if (!wasAdded)
                        cookieDictionary["sessionid"] = result["sessionid"];
                }
            }

            //cookieDictionary.Add(cookieToAdd.Name, cookieToAdd.Value);

            if (result["success"] == "true")
            {

                SteamController steamController = new SteamController();

                // steamController.GetAPIKey(cookieDictionary);
                Cookie cookieToAdd = new Cookie("ApiKeyNeeded","true");
                HttpContext.Response.Cookies.Append(cookieToAdd.Name, cookieToAdd.Value, new CookieOptions { IsEssential = true });

                return RedirectToPage("AuthKey", new { _emailAuth = false, _twoFactor = false, _challenge = "", _authkey = true }); 
            }
            else
            {
                return RedirectToPage("Login"); 
            }
        }

    }
}
