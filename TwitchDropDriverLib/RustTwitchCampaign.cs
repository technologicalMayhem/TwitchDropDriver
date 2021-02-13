using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace TwitchDropDriverLib
{
    public static class RustTwitchCampaign
    {
        private const string FacepunchTwitchUrl = "https://twitch.facepunch.com/";

        public static IEnumerable<string> GetStreamsWithDrops()
        {
            return new HtmlWeb().Load(FacepunchTwitchUrl)
                .DocumentNode.SelectNodes("//div[@class=\"rust-drops streamer animate__fadeInDown\"]//a[@href]")
                .Select(link => link.GetAttributeValue("href", string.Empty))
                .Distinct()
                .Where(s => s.StartsWith("https://www.twitch.tv/"))
                .Select(s => s.Replace("https://www.twitch.tv/", string.Empty))
                .ToArray();
        }

        public static DateTime GetResetTime()
        {
            var timeNode = new HtmlWeb().Load(FacepunchTwitchUrl).DocumentNode
                .SelectSingleNode("//div[@class=\"time-date animate__fadeInDown\"]//span//strong[2]");
            var parts = timeNode.InnerText.Split(' ');
            var month = parts[0];
            var day = Regex.Match(parts[1], @"\d+").Value;
            var time = parts[2];
            return DateTime.ParseExact($"{month} {day} {time}", "MMMM d hh:mmtt",
                CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}