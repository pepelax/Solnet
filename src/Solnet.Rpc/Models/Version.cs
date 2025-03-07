using MessagePack;
using System.Text.Json.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the current solana versions running on the node.
    /// </summary>
    [MessagePackObject]
    public class NodeVersion
    {
        /// <summary>
        /// Software version of solana-core.
        /// </summary>
        [JsonPropertyName("solana-core")]
        [Key(0)]
        public string SolanaCore { get; set; }

        /// <summary>
        /// unique identifier of the current software's feature set.
        /// </summary>
        [JsonPropertyName("feature-set")]
        [Key(1)]
        public ulong? FeatureSet { get; set; }
    }
}