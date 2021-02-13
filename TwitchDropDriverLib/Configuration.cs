using System;
using System.IO;
using System.Text.Json;

namespace TwitchDropDriverLib
{
    public class Configuration
    {
        public string Identifier { get; set; } = "ChangeMe";
        
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;

        public int Watchtime { get; set; } = 130;

        public EmailConfiguration Email { get; set; } = new();
        
        private const string ConfigurationJson = "configuration.json";

        public static Configuration LoadConfiguration()
        {
            if (!File.Exists(ConfigurationJson))
            {
                new Configuration().SaveConfiguration();
                Console.WriteLine(
                    $"Please configure the application by editing {ConfigurationJson}, then start this again.");
                Environment.Exit(1);
            }

            var configuration = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(ConfigurationJson));
            if (configuration is not null) return configuration;

            Console.WriteLine("Could not load configuration.");
            Environment.Exit(1);
            return null;
        }

        public void SaveConfiguration()
        {
            File.WriteAllText(ConfigurationJson, JsonSerializer.Serialize(this,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
        }
    }

    public class EmailConfiguration
    {
        public string To { get; set; } = "example@example.com";
        public string SmtpAddress { get; set; } = "127.0.0.1";
        public int SmtpPort { get; set; } = 25;
    }
}