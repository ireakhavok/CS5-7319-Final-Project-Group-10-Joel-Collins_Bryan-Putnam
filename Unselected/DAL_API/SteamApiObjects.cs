using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DAL_API
{
    public class SingleValueArrayConverter<T> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object retVal = new Object();
            if (reader.TokenType == JsonToken.StartObject)
            {
                T instance = (T)serializer.Deserialize(reader, typeof(T));
                retVal = new List<T>() { instance };
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                retVal = serializer.Deserialize(reader, objectType);
            }
            return retVal;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
    public class PlayerGamesListResponse
    {
        public PlayerGamesListResponse()
        {

        }
        public PlayerGamesList response { get; set; }

    }
    public class PlayerGamesList
    {
        public int game_count { get; set; }
        public IList<PlayerGame>? games { get; set; }
    }
    public class PlayerGame
    {
        //"rtime_last_played":0,"playtime_disconnected":0},
        public int appid { get; set; }
        public string? Playtime { get; set; }
        public int playtime_forever { get; set; }
        public int playtime_windows_forever { get; set; }
        public int playtime_mac_forever { get; set; }
        public int playtime_linux_forever { get; set; }
        public int rtime_last_played { get; set; }
        public int playtime_disconnected { get; set; }
        public PlayerCountResponse? PlayerCountResponse { get; set; }
        public string? Name { get; set; }
        public DateTime? LastNews { get; set; }
        public DateTime? LastDatePlayed { get; set; }
        public int? PlayerCount { get; set; }
        public NewsForAppResponse? NewsForAppResponse { get; set; }
        public IDictionary<string,GameInfoResponse>? GameInfoResponse { get; set; }

        public int? MetacriticScore { get; set; }
        public string? MetacriticURL { get; set; }

    }
    public class NewsForAppResponse
    {
        public NewsForAppResponse()
        {

        }
        public AppNews appnews { get; set; }
    }
    public class AppNews
    {
        public int appid { get; set; }
        public IList<NewsItem>? newsitems { get; set; }
        public int? count { get; set; }
    }
    public class NewsItem
    {
        public string? gid { get; set; }
        public string? title { get; set; }
        public string? url { get; set; }
        public bool is_external_url { get; set; }
        public string? author { get; set; }
        public string? contents { get; set; }
        public string? feedlabel { get; set; }
        //TODO: this needs to be in time since 1970
        public int date { get; set; }
        public string? feedname { get; set; }
        public int feed_type { get; set; }

    }
    public class PlayerCountResponse
    {
        public Player_Count? response { get; set; }
    }
    public class Player_Count
    {
        public int player_count { get; set; }
        public int result { get; set; }
    }
    public class GameInfoResponse
    {
        public bool success { get; set; }
        public GameInfoData? data { get; set; }
    }
    public class App
    {
        public int appid { get; set; }
        public string name { get; set; }
    }

    public class Applist
    {
        public List<App> apps { get; set; }
    }

    public class ApplistRoot
    {
        public Applist applist { get; set; }
    }
    public class GameInfoData
    {
        public string? type { get; set; }
        public string? name { get; set; }
        public int steam_appid { get; set; }
        public string? required_age { get; set; }
        public bool is_free { get; set; }
        public string? detailed_description { get; set; }
        public string? supported_languages { get; set; }
        public string? header_image { get; set; }
        public string? capsule_image { get; set; }
        public string? capsule_imagev5 { get; set; }
        public string? website { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<PC_Requirements>))]
        public IList<PC_Requirements>? pc_requirements { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<Mac_Requirements>))]
        public IList <Mac_Requirements>? mac_requirements { get; set; }
        [JsonConverter(typeof(SingleValueArrayConverter<Linux_Requirements>))]
        public IList<Linux_Requirements>? linux_requirements { get; set; }
        public string[]? developers { get; set; }
        public string[]? publishers { get; set; }
        public Demo[]? demos { get; set; }
        public Price_Overview? price_overview { get; set; }
        public int[]? packages { get; set; }
        public Package[]? package_groups { get; set; }
        public Platforms? platforms { get; set; }
        public Metacritic? metacritic { get; set; }
        public Category[]? categories { get; set; }
        public Genre[]? genres { get; set; }
        public Screenshot[]? screenshots { get; set; }
        public Recomendation? recommendations { get; set; }
        public Release_Date? release_date { get; set; }
        public Support_Info? support_info { get; set; }
        public string? background { get; set; }
        public string? background_raw { get; set; }

    }
    public class PC_Requirements
    {
        public string? minimum { get; set; }

    }
    public class Mac_Requirements 
    {
        public string? minimum { get; set; }
    }
    public class Linux_Requirements
    {
        public string? minimum { get; set; }
    }
    //public class string[]? developers { get; set; }
    //public class string[]? publishers { get; set; }

    //public class Content_descriptors
    //{
    //    public IList<int?>? ids { get; set; }
    //    public string? notes { get; set; }
    //}
    public class Support_Info
    {
        public string? url { get; set; }
        public string? email { get; set; }
    }
    public class Release_Date
    {
        public bool? coming_soon { get; set; }
        public string? date { get; set; }
    }
    public class Recomendation
    {
        public int? total { get; set; }
    }
    public class Screenshot
    {
        public int? id { get; set; }
        public string? path_thumbnail { get; set; }
        public string? path_full { get; set; }
    }
    public class Genre
    {
        public string? id { get; set; }
        public string? description { get; set; }
    }
    public class Category
    {
        public int? id { get; set; }
        public string? description { get; set; }
    }
    public class Metacritic
    {
        public int score { get; set; }
        public string? url { get; set; }

    }
    public class Platforms
    {
        public bool? windows { get; set; }
        public bool? mac { get; set; }
        public bool? linux { get; set; }
    }
    public class Package
    {
        public string? name { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? selection_text { get; set; }
        public string? save_text { get; set; }
        public int? display_type { get; set; }
        public string? is_recurring_subscription { get; set; }
        
        public IList<Subs>? subs { get; set; }
    }
    public class Subs
    {
        public int? packageid { get; set; }
        public string? percent_savings_text { get; set; }
        public int? percent_savings { get; set; }
        public string? option_text { get; set; }
        public string? option_description { get; set; }
        public string? can_get_free_license { get; set; }
        public bool? is_free_license { get; set; }
        public int? price_in_cents_with_discount { get; set; }

    }
    public class Price_Overview
    {
        public string? currency { get; set; }
        public int? initial { get; set; }
        public int? discount_percent { get; set; }
        public string? initial_formatted { get; set; }
        public string? final_formatted { get; set; }
    }

    public class Demo
    {
        public int? appid { get; set; }
        public string? description { get; set; }
    }
}
