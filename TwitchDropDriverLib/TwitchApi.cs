using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwitchDropDriverLib
{
    public class TwitchApi
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly HttpClient _client;
        private DateTime _tokenValidUntil;

        private const string Json = "application/json";

        public TwitchApi(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;

            _client = new HttpClient();
        }

        private async Task Login()
        {
            var result = await PostAsync<AuthResponse>(
                $"https://id.twitch.tv/oauth2/token?client_id={_clientId}&client_secret={_clientSecret}&grant_type=client_credentials",
                null);
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {result.AccessToken}");
            _client.DefaultRequestHeaders.Add("client-id", _clientId);
            _tokenValidUntil = DateTime.Now + TimeSpan.FromSeconds(result.ExpiresIn - 60);
        }

        private async Task EnsureLoggedIn()
        {
            if (DateTime.Now > _tokenValidUntil)
            {
                await Login();
            }
        }

        public async Task<Streams> GetStreamInfoByUsername(IEnumerable<string> usernames)
        {
            await EnsureLoggedIn();
            return await GetAsync<Streams>(
                $"https://api.twitch.tv/helix/streams?user_login={string.Join("&user_login=", usernames)}");
        }

        private async Task<TResult> GetAsync<TResult>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<TResult>(responseStream);
        }

        private async Task<HttpResponseMessage> PostAsync(string url, object model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Json);
            var response = await _client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<TResult> PostAsync<TResult>(string url, object model)
        {
            var response = await PostAsync(url, model);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(responseString);
        }

        private class AuthResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }

            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }
        }

        public class StreamerData
        {
            [JsonPropertyName("user_login")]
            public string UserLogin { get; set; }

            [JsonPropertyName("game_name")]
            public string GameName { get; set; }

            public override string ToString()
            {
                return UserLogin;
            }
        }

        public class Streams
        {
            [JsonPropertyName("data")]
            public List<StreamerData> Data { get; set; }
        }
    }
}