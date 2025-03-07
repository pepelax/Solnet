// ReSharper disable ClassNeverInstantiated.Global
using MessagePack;
using System.Text.Json.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents a node in the cluster.
    /// </summary>
    [MessagePackObject]
    public class ClusterNode
    {
        /// <summary>
        /// Gossip network address for the node.
        /// </summary>
        [Key(0)]
        public string Gossip { get; set; }

        /// <summary>
        /// A base-58 encoded public key associated with the node.
        /// </summary>
        [JsonPropertyName("pubkey")]
        [Key(1)]
        public string PublicKey { get; set; }

        /// <summary>
        /// JSON RPC network address for the node. The service may not be enabled.
        /// </summary>
        [Key(2)]
        public string Rpc { get; set; }

        /// <summary>
        /// TPU network address for the node.
        /// </summary>
        [Key(3)]
        public string Tpu { get; set; }

        /// <summary>
        /// The software version of the node. The information may not be available.
        /// </summary>
        [Key(4)]
        public string Version { get; set; }

        /// <summary>
        /// Unique identifier of the current software's feature set.
        /// </summary>
        [Key(5)]
        public ulong? FeatureSet { get; set; }

        /// <summary>
        /// The shred version the node has been configured to use.
        /// </summary>
        [Key(6)]
        public ulong ShredVersion { get; set; }
    }
}