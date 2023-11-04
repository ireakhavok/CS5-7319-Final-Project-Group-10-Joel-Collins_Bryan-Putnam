using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL_API
{

    public class SteamController
    {
        public string? apiKey { get; set; }
        public PlayerGamesListResponse? gameList;
        public ApplistRoot? appListResponse;
        public static List<Genre>? genres = new List<Genre>();
        public string AcceptLanguageHeader { get { return acceptLanguageHeader; } set { acceptLanguageHeader = value; } }
        private string acceptLanguageHeader = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en" ? Thread.CurrentThread.CurrentCulture.ToString() + ",en;q=0.8" : Thread.CurrentThread.CurrentCulture.ToString() + "," + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + ";q=0.8,en;q=0.6";


        /// <summary>
        /// Custom wrapper for creating a HttpWebRequest, edited for Steam.
        /// </summary>
        /// <param name="url">Gets information about the URL of the current request.</param>
        /// <param name="method">Gets the HTTP data transfer method (such as GET, POST, or HEAD) used by the client.</param>
        /// <param name="data">A NameValueCollection including Headers added to the request.</param>
        /// <param name="ajax">A bool to define if the http request is an ajax request.</param>
        /// <param name="referer">Gets information about the URL of the client's previous request that linked to the current URL.</param>
        /// <param name="fetchError">Return response even if its status code is not 200</param>
        /// <returns>An instance of a HttpWebResponse object.</returns>
        public HttpWebResponse Request(string url, string method, NameValueCollection data = null, bool ajax = true, string referer = "", bool fetchError = false)
        {
            // Append the data to the URL for GET-requests.
            bool isGetMethod = (method.ToLower() == "get");
            string dataString = (data == null ? null : String.Join("&", Array.ConvertAll(data.AllKeys, key =>
                // ReSharper disable once UseStringInterpolation
                string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(data[key]))
            )));

            // Example working with C# 6
            // string dataString = (data == null ? null : String.Join("&", Array.ConvertAll(data.AllKeys, key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(data[key])}" )));

            // Append the dataString to the url if it is a GET request.
            if (isGetMethod && !string.IsNullOrEmpty(dataString))
            {
                url += (url.Contains("?") ? "&" : "?") + dataString;
            }

            // Setup the request.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.Accept = "application/json, text/javascript;q=0.9, */*;q=0.5";
            request.Headers[HttpRequestHeader.AcceptLanguage] = AcceptLanguageHeader;
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            // request.Host is set automatically.
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            request.Referer = string.IsNullOrEmpty(referer) ? "http://steamcommunity.com/trade/1" : referer;
            request.Timeout = 50000; // Timeout after 50 seconds.
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            // If the request is an ajax request we need to add various other Headers, defined below.
            if (ajax)
            {
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("X-Prototype-Version", "1.7");
            }

            // Cookies
            //request.CookieContainer = _cookies;

            // If the request is a GET request return now the response. If not go on. Because then we need to apply data to the request.
            if (isGetMethod || string.IsNullOrEmpty(dataString))
            {
                return request.GetResponse() as HttpWebResponse;
            }

            // Write the data to the body for POST and other methods.
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);
            request.ContentLength = dataBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(dataBytes, 0, dataBytes.Length);
            }

            // Get the response and return it.
            try
            {
                return request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                //this is thrown if response code is not 200
                if (fetchError)
                {
                    var resp = ex.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        return resp;
                    }
                }

                throw;
            }
        }
        public SteamController()
        {
            gameList = new PlayerGamesListResponse();
        }
        public async void GetAPIKey(Dictionary<string,string> collection)
        {
            var baseAddress = new Uri("https://steamcommunity.com/dev/apikey");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                for (var i = 0; i < collection.Count; i++)
                {
                    cookieContainer.Add(baseAddress, new Cookie(collection.ElementAt(i).Key, collection.ElementAt(i).Value));
                }
                var result = await client.GetAsync(baseAddress);
                result.EnsureSuccessStatusCode();
            }


            ////https://steamcommunity.com/dev/apikey
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response =
            //        await httpClient.GetAsync(
            //            "https://steamcommunity.com/dev/apikey"))
            //    {
            //        string apiResponseString = await response.Content.ReadAsStringAsync();
            //        //gameList = JsonConvert.DeserializeObject<PlayerGamesListResponse>(apiResponseString);
            //        //gameList.response.games.OrderByDescending(x => x.playtime_forever);
            //        return;

            //    }
            //}
        }
        public async Task getInfoForGamesAsync()
        {
            try
            {
                await getInfoForGames();
            }
            catch
            {
                await getAllApps();
            }
        }
        public void getInfoForGamesNonAsync()
        {
            try
            {
                _getInfoForGamesNonAsync();
            }
            catch
            {
                getAllApps();
            }
        }
        public async void getNewsDatesForGamesAsync()
        {
            await getNewsDatesForGames();
        }
        public async void getPlayerCountForGamesAsync()
        {
            await getPlayerCountForGames();
        }
        public async void getNewsDatesForGamesNonAsync()
        {
             _getNewsDatesForGamesNonAsync();
        }
        public async void getPlayerCountForGamesNonAsync()
        {
             _getPlayerCountForGamesNonAsync();
        }
        //https://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=AAF472F9B2494C943447F4488C912D6D&steamid=76561198254673414&relationship=friend
        /// <summary>
        /// /
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        public async Task GetGames(string steamID)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = 
                    await httpClient.GetAsync(
                        "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key="+apiKey+"&steamid="+steamID+"&format=json"))
                {
                    string apiResponseString = await response.Content.ReadAsStringAsync();
                    gameList = JsonConvert.DeserializeObject<PlayerGamesListResponse>(apiResponseString);
                    gameList.response.games.OrderByDescending(x => x.playtime_forever);
                   
                }
            }
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="steamID"></param>
        /// <returns></returns>
        public void GetGamesNonAsync(string steamID)
        {
            using (HttpWebResponse webResponse = Request("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + apiKey + "&steamid=" + steamID + "&format=json", "GET"))
            {
                var stream = webResponse.GetResponseStream();
                //string apiResponseString = response.Content.ReadAsStringAsync();
                string res;
                using (var sr = new StreamReader(stream))
                {
                    res = sr.ReadToEnd();
                }
                gameList = JsonConvert.DeserializeObject<PlayerGamesListResponse>(res.ToString());
                gameList.response.games.OrderByDescending(x => x.playtime_forever);
            }
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response =
            //        httpClient.get(
            //            "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + apiKey + "&steamid=" + steamID + "&format=json"))
            //    {
            //        string apiResponseString =  response.Content.ReadAsStringAsync();
            //        gameList = JsonConvert.DeserializeObject<PlayerGamesListResponse>(apiResponseString);
            //        gameList.response.games.OrderByDescending(x => x.playtime_forever);

            //    }
            //}
        }
        public async Task getFriends()
        {
            using (var httpClient = new HttpClient())
            {


                using (var gameInfoResponse =
                    await httpClient.GetAsync("https://api.steampowered.com/ISteamApps/GetAppList/v2/"))
                {
                    string gameInfoResponseString = await gameInfoResponse.Content.ReadAsStringAsync();
                    //GET https://partner.steam-api.com/ISteamUser/GetFriendList/v1/
                    //
                }
            }
        }
        public async Task getAllApps()
        {
            using (var httpClient = new HttpClient())
            {


                using (var gameInfoResponse =
                    await httpClient.GetAsync("https://api.steampowered.com/ISteamApps/GetAppList/v2/"))
                {
                    string gameInfoResponseString = await gameInfoResponse.Content.ReadAsStringAsync();
                    //dynamic gameInfo = JsonConvert.DeserializeObject(gameInfoResponseString);

                    appListResponse
                        = JsonConvert.DeserializeObject<ApplistRoot>(gameInfoResponseString);

                    for (int i = 0; i < gameList.response.games.Count; i++)
                    //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                    {

                        int indexLoc = appListResponse.applist.apps.FindIndex(q => q.appid == gameList.response.games[i].appid);
                        //gameList.Applist gameList.response.games[i].appid 
                        if (indexLoc != -1)
                        {
                            App application = appListResponse.applist.apps[indexLoc];

                            gameList.response.games[i].Name = application.name;
                        }
                        else
                        {
                            gameList.response.games[i].Name = "<Custom Game>";
                        }


                    }

                }

            }
        }
        public void _getInfoForGamesNonAsync()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddSeconds(gameList.response.games[i].rtime_last_played).ToLocalTime();
                    TimeSpan span = TimeSpan.FromMinutes(gameList.response.games[i].playtime_forever);
                    string label = span.ToString(@"dd\:hh\:mm\:ss");
                    gameList.response.games[i].Playtime = label;
                    gameList.response.games[i].LastDatePlayed = dateTime;
                    using (HttpWebResponse webResponse = Request("https://store.steampowered.com/api/appdetails?appids="
                        + gameList.response.games[i].appid, "GET"))
                    {
                        var stream = webResponse.GetResponseStream();
                        string res;
                        using (var sr = new StreamReader(stream))
                        {
                            res = sr.ReadToEnd();
                        }
                        //string apiResponseString = response.Content.ReadAsStringAsync();
                        try
                        {
                            gameList.response.games[i].GameInfoResponse
                                = JsonConvert.DeserializeObject<Dictionary<string, GameInfoResponse>>(res);

                        }
                        catch
                        {
                            continue;
                        }
                        gameList.response.games[i].GameInfoResponse.TryGetValue(gameList.response.games[i].appid.ToString(),
                           out GameInfoResponse gameInfoValue);
                        if (gameInfoValue.success == true)
                        {
                            gameList.response.games[i].Name = gameInfoValue.data.name;
                            if (gameInfoValue.data.metacritic != null)
                            {
                                gameList.response.games[i].MetacriticScore = gameInfoValue.data.metacritic.score;
                                gameList.response.games[i].MetacriticURL = gameInfoValue.data.metacritic.url;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
        public async Task getInfoForGames()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddSeconds(gameList.response.games[i].rtime_last_played).ToLocalTime();
                    TimeSpan span = TimeSpan.FromMinutes(gameList.response.games[i].playtime_forever);
                    string label = span.ToString(@"dd\:hh\:mm\:ss");
                    gameList.response.games[i].Playtime = label;
                    gameList.response.games[i].LastDatePlayed = dateTime;

                    using (var gameInfoResponse =
                        await httpClient.GetAsync("https://store.steampowered.com/api/appdetails?appids="
                        + gameList.response.games[i].appid))
                    {
                        string gameInfoResponseString = await gameInfoResponse.Content.ReadAsStringAsync();
                        //dynamic gameInfo = JsonConvert.DeserializeObject(gameInfoResponseString);
                        try
                        {
                            gameList.response.games[i].GameInfoResponse
                                = JsonConvert.DeserializeObject<Dictionary<string, GameInfoResponse>>(gameInfoResponseString);

                        }
                        catch
                        {
                            continue;
                        }


                        gameList.response.games[i].GameInfoResponse.TryGetValue(gameList.response.games[i].appid.ToString(),
                            out GameInfoResponse gameInfoValue);
                        if (gameInfoValue.success == true)
                        {
                            gameList.response.games[i].Name = gameInfoValue.data.name;
                            if (gameInfoValue.data.metacritic != null)
                            {
                                gameList.response.games[i].MetacriticScore = gameInfoValue.data.metacritic.score;
                                gameList.response.games[i].MetacriticURL = gameInfoValue.data.metacritic.url;

                                //THIS caused the thread to crash?
                                //foreach (Genre genreItem in gameInfoValue.data.genres)
                                //{
                                //    if (genres.Count == 0)
                                //    {
                                //        genres.Add(genreItem);
                                //    }
                                //    foreach (Genre genreFromList in genres)
                                //    {
                                //        if (genreFromList.id == genreItem.id)
                                //        {
                                //            continue;
                                //        }
                                //        else
                                //        {
                                //            genres.Add(genreItem);
                                //        }
                                //    }
                                //}
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
        public async Task getFriendsList(string steamID)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response =
                    await httpClient.GetAsync(
                        "http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key="+apiKey+"&steamid=" + steamID + "&format=json"))
                {
                    string apiResponseString = await response.Content.ReadAsStringAsync();
                    gameList = JsonConvert.DeserializeObject<PlayerGamesListResponse>(apiResponseString);
                    gameList.response.games.OrderByDescending(x => x.playtime_forever);

                }
            }

            Console.Write(gameList);
        }
    
        public async Task getPlayerCountForGames()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    using (var playerCountResponse =
                  await httpClient.GetAsync("https://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?appid="
                  + gameList.response.games[i].appid))
                    {
                        string playerCountResponseString = await playerCountResponse.Content.ReadAsStringAsync();
                        gameList.response.games[i].PlayerCountResponse
                        = JsonConvert.DeserializeObject<PlayerCountResponse>(playerCountResponseString);
                        gameList.response.games[i].PlayerCount = gameList.response.games[i].PlayerCountResponse.response.player_count;
                    }
                }
            }
        }
        public async Task _getPlayerCountForGamesNonAsync()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    using (HttpWebResponse webResponse = Request("https://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?appid="
                  +gameList.response.games[i].appid, "GET"))
                    {
                        var stream = webResponse.GetResponseStream();
                        string res;
                        using (var sr = new StreamReader(stream))
                        {
                            res = sr.ReadToEnd();
                        }
                        gameList.response.games[i].PlayerCountResponse
                       = JsonConvert.DeserializeObject<PlayerCountResponse>(res);
                        gameList.response.games[i].PlayerCount = gameList.response.games[i].PlayerCountResponse.response.player_count;
                    }
                }
            }
        }
        public async Task getNewsForASingleGame(PlayerGame game)
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    using (var newsResponse =
                           await httpClient.GetAsync("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid="
                           + gameList.response.games[i].appid + "&count=1&maxlength=3000&format=json"))
                    {
                        string newsResponseString = await newsResponse.Content.ReadAsStringAsync();

                        gameList.response.games[i].NewsForAppResponse
                        = JsonConvert.DeserializeObject<NewsForAppResponse>(newsResponseString);

                        //gameList = JsonConvert.DeserializeObject<NewsResponse>(newsResponseString);

                    }
                    if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count() > 0)
                    {
                        int maxDate = 0;
                        for (int j = 0; j < gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count(); j++)
                        {
                            if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date > maxDate)
                                maxDate = gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date;
                        }
                        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        dateTime2 = dateTime2.AddSeconds(maxDate).ToLocalTime();
                        gameList.response.games[i].LastNews = dateTime2;
                    }
                }
            }
        }
        public async Task getNewsForGames()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    using (var newsResponse =
                           await httpClient.GetAsync("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid="
                           + gameList.response.games[i].appid + "&count=1&maxlength=3000&format=json"))
                    {
                        string newsResponseString = await newsResponse.Content.ReadAsStringAsync();

                        gameList.response.games[i].NewsForAppResponse
                        = JsonConvert.DeserializeObject<NewsForAppResponse>(newsResponseString);

                        //gameList = JsonConvert.DeserializeObject<NewsResponse>(newsResponseString);

                    }
                    if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count() > 0)
                    {
                        int maxDate = 0;
                        for (int j = 0; j < gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count(); j++)
                        {
                            if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date > maxDate)
                                maxDate = gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date;
                        }
                        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        dateTime2 = dateTime2.AddSeconds(maxDate).ToLocalTime();
                        gameList.response.games[i].LastNews = dateTime2;
                    }
                }
            }
        }
        public void _getNewsDatesForGamesNonAsync()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    using (HttpWebResponse webResponse = Request("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid="
                           + gameList.response.games[i].appid + "&count=1&maxlength=300&format=json", "GET"))
                    {
                        var stream = webResponse.GetResponseStream();
                        string res;
                        using (var sr = new StreamReader(stream))
                        {
                            res = sr.ReadToEnd();
                        }
                        gameList.response.games[i].NewsForAppResponse
                        = JsonConvert.DeserializeObject<NewsForAppResponse>(res);
                    }

                    if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count() > 0)
                    {
                        int maxDate = 0;
                        for (int j = 0; j < gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count(); j++)
                        {
                            if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date > maxDate)
                                maxDate = gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date;
                        }
                        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        dateTime2 = dateTime2.AddSeconds(maxDate).ToLocalTime();
                        gameList.response.games[i].LastNews = dateTime2;
                    }
                }
            }
        }
        public async Task getNewsDatesForGames()
        {
            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < gameList.response.games.Count; i++)
                //foreach (PlayerGame gameList.response.games[i] in gameList.response.games.OrderByDescending(x => x.playtime_forever))
                {
                    using (var newsResponse =
                           await httpClient.GetAsync("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid="
                           + gameList.response.games[i].appid + "&count=1&maxlength=300&format=json"))
                    {
                        string newsResponseString = await newsResponse.Content.ReadAsStringAsync();

                        gameList.response.games[i].NewsForAppResponse
                        = JsonConvert.DeserializeObject<NewsForAppResponse>(newsResponseString);

                        //gameList = JsonConvert.DeserializeObject<NewsResponse>(newsResponseString);

                    }
                    if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count() > 0)
                    {
                        int maxDate = 0;
                        for (int j = 0; j < gameList.response.games[i].NewsForAppResponse.appnews.newsitems.Count(); j++)
                        {
                            if (gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date > maxDate)
                                maxDate = gameList.response.games[i].NewsForAppResponse.appnews.newsitems[j].date;
                        }
                        DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        dateTime2 = dateTime2.AddSeconds(maxDate).ToLocalTime();
                        gameList.response.games[i].LastNews = dateTime2;
                    }
                }
            }
        }
    }
}
