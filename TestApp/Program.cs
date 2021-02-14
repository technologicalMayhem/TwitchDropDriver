using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchDropDriverLib.Data;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var oAuth = QueryUser("OAuth");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("OAuth " + oAuth);
            var model = TwitchOperation.GetDropStatusOperations;
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var responseMessage = await httpClient.PostAsync("https://gql.twitch.tv/gql", content);
            responseMessage.EnsureSuccessStatusCode();

            var responseContent = await responseMessage.Content.ReadAsStreamAsync();

            var response = await JsonSerializer.DeserializeAsync<OperationResponse[]>(responseContent);

            if (response is not null)
            {
                var inventory = response.First().ResponseData.User.Inventory;
                Console.WriteLine("=== Drops in progress ===");
                if (inventory.DropCampaignsInProgress is not null)
                    foreach (var dropCampaign in inventory.DropCampaignsInProgress)
                    {
                        var minutesWatched = dropCampaign.TimeBasedDropSelfEdge.CurrentMinutesWatched;
                        var requiredMinutes = dropCampaign.TimeBasedDrops.First().RequiredMinutesWatched;
                        var percentage = (float) minutesWatched / requiredMinutes;
                        Console.WriteLine($"{dropCampaign.Name} {minutesWatched}/{requiredMinutes} ({percentage:P})");
                    }
                else Console.WriteLine("No campaign drops in progress.");

                Console.WriteLine();
                Console.WriteLine("=== Obtained Drops ===");
                if (inventory.GameEventDrops is not null)
                    foreach (var eventDrop in inventory.GameEventDrops)
                    {
                        Console.WriteLine($"{eventDrop.LastAwardedAt:g} - {eventDrop.Name}");
                    }
                else Console.WriteLine("Not campaign drops obtained.");
            }
        }

        private static string QueryUser(string message)
        {
            Console.Write($"{message}: ");
            return Console.ReadLine();
        }
    }
}