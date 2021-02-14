using System.Text.Json.Serialization;

namespace TwitchDropDriverLib.Data
{
    public class PersistedQuery
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("sha256Hash")]
        public string Sha256Hash { get; set; }
    }

    public class OperationExtensions
    {
        [JsonPropertyName("persistedQuery")]
        public PersistedQuery PersistedQuery { get; set; }
    }

    public class TwitchOperation
    {
        [JsonPropertyName("operationName")]
        public string OperationName { get; set; }

        [JsonPropertyName("extensions")]
        public OperationExtensions Extensions { get; set; }

        public static TwitchOperation[] GetDropStatusOperations =>
            new[]
            {
                GetInventoryOperation
            };

        // private static TwitchOperation GetDropOperation =>
        //     new()
        //     {
        //         OperationName = "DropCurrentSessionContext",
        //         Extensions = new Extensions
        //         {
        //             PersistedQuery = new PersistedQuery
        //             {
        //                 Version = 1,
        //                 Sha256Hash = "2e4b3630b91552eb05b76a94b6850eb25fe42263b7cf6d06bee6d156dd247c1c"
        //             }
        //         }
        //     };


        private static TwitchOperation GetInventoryOperation =>
            new()
            {
                OperationName = "Inventory",
                Extensions = new OperationExtensions
                {
                    PersistedQuery = new PersistedQuery
                    {
                        Version = 1,
                        Sha256Hash = "e0765ebaa8e8eeb4043cc6dfeab3eac7f682ef5f724b81367e6e55c7aef2be4c"
                    }
                }
            };
    }
}