using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TwitchDropDriverLib
{
    public class TwitchWatcher
    {
        private readonly Configuration _configuration;
        private readonly ILogger _logger;
        private readonly TwitchApi _twitch;
        private readonly Dictionary<string, int> _watchTime = new();

        public TwitchWatcher(Configuration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _twitch = new TwitchApi(_configuration.ClientId, _configuration.ClientSecret);
        }

        public async Task WatchStreams(IEnumerable<string> usernames, CancellationToken cancellationToken)
        {
            var channelsToWatch = usernames as List<string> ?? usernames.ToList();
            using var driver = new FirefoxDriver() as IWebDriver;

            EnsureLoggedIn(driver);
            foreach (var channel in channelsToWatch)
            {
                _watchTime[channel] = 0;
            }

            while (channelsToWatch.Count > 0 && !cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Channels still needing to be watched: {string.Join(", ", channelsToWatch)}");

                var streams = await _twitch.GetStreamInfoByUsername(channelsToWatch);
                var streamersOnline = streams.Data
                    .Where(streamer => 
                        channelsToWatch.Contains(streamer.UserLogin.ToLower()) &&
                        streamer.GameName.ToLower() == "rust")
                    .ToList();

                if (streamersOnline.Any())
                {
                    _logger.LogInformation($"Online right now: {string.Join(',', streamersOnline)}");
                    var username = streamersOnline.First().UserLogin;
                    await WatchStream(username, driver, cancellationToken);
                }
                else
                {
                    _logger.LogInformation("No one is online right now.");
                    await Task.Delay(TimeSpan.FromMinutes(10), cancellationToken);
                }

                //Remove channels that have been watched enough
                foreach (var (username, watchTime) in _watchTime)
                {
                    if (watchTime >= _configuration.Watchtime)
                        channelsToWatch.Remove(username);
                }
            }
        }

        private async Task WatchStream(string username, IWebDriver driver, CancellationToken cancellationToken)
        {
            driver.Navigate().GoToUrl($"https://www.twitch.tv/{username}");
            _logger.LogInformation($"Starting to watch {username}.");
            while (await IsStillEligible(username))
            {
                await Task.Delay(TimeSpan.FromMinutes(10), cancellationToken);
                _watchTime[username] += 10;
                if (_watchTime[username] <= _configuration.Watchtime) continue;
                _logger.LogInformation($"Finished watching {username}");
                return;
            }

            //If the stream is no longer eligible we are just gonna assume the last 10 minutes didn't count.
            _watchTime[username] -= 10;
        }

        private async Task<bool> IsStillEligible(string username)
        {
            var info = await _twitch.GetStreamInfoByUsername(new[] {username});
            return info.Data.Any(data => data.GameName == "Rust");
        }

        private static void EnsureLoggedIn(IWebDriver driver)
        {
            // if (driver.Manage().Cookies.AllCookies.Any(cookie => cookie.Value.Contains("api_token")))
            //     return;

            Console.WriteLine("Please ensure that you are logged into twitch. Then press enter.");
            driver.Navigate().GoToUrl("https://twitch.tv/login");

            Console.ReadLine();

            // while (true)
            // {
            //     if (driver.Manage().Cookies.AllCookies.Any(cookie => cookie.Value.Contains("api_token")))
            //         return;
            //
            //     Thread.Sleep(1000);
            // }
        }
    }
}