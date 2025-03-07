using System.Text.Json;
using System.Text.Json.Serialization;
using Solnet.Rpc.Converters;

namespace Solnet.Rpc.Core
{
    /// <summary>
    /// Static class that provides shared JsonSerializerOptions for RPC operations.
    /// </summary>
    public static class JsonRpcSerializerOptions
    {
        /// <summary>
        /// The default JsonSerializerOptions used for RPC operations.
        /// </summary>
        public static JsonSerializerOptions Default { get; } = new()
        {
            // MaxDepth = 64,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new EncodingConverter(),
                // new TransactionMetaInfoConverter(),
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
    }
}
