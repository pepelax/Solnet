using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the identity public key for the current node.
    /// </summary>
    [MessagePackObject]
    public class NodeIdentity
    {
        /// <summary>
        /// The identity public key of the current node, as base-58 encoded string.
        /// </summary>
        [Key(0)]
        public string Identity { get; set; }
    }
}