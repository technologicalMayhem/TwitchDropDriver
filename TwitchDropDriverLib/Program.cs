using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchDropDriverLib
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configuration = Configuration.LoadConfiguration();
            var logger = new Logger();
            var notifier = new EmailNotifier(configuration.Email);
            var twitchWatcher = new TwitchWatcher(configuration, logger);

            try
            {
                while (true)
                {
                    var resetTime = RustTwitchCampaign.GetResetTime();
                    var streamsWithDrops = RustTwitchCampaign.GetStreamsWithDrops()
                        .Except(args)
                        .Select(s => s.ToLower())
                        .ToArray();

                    logger.LogInformation($"Reset time is {resetTime:f}.");
                    logger.LogInformation("Streamers with loot are:");
                    foreach (var streamWithDrop in streamsWithDrops)
                    {
                        logger.LogInformation($"  {streamWithDrop}");
                    }

                    await twitchWatcher.WatchStreams(streamsWithDrops, CancellationToken.None);
                    await notifier.Notify($"{configuration.Identifier}: Watching done",
                        $"{configuration.Identifier} has finished watching it's streams. Please check if all drops have been obtained. It will now wait for the next round of drops.");

                    var timeUntilReset = resetTime - DateTime.Now;
                    await Task.Delay(timeUntilReset);
                }
            }
            catch (Exception e)
            {
                await notifier.Notify($"{configuration.Identifier}: Exception occured", e.Message);

                var streamWriter = new StreamWriter("exception.txt");
                await streamWriter.WriteLineAsync(e.Message);
                await streamWriter.WriteLineAsync(e.StackTrace);
                streamWriter.Close();
            }
        }
    }
}