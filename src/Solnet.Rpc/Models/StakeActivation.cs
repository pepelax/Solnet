using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the stake activation info.
    /// </summary>
    [MessagePackObject]
    public class StakeActivationInfo
    {
        /// <summary>
        /// Stake active during the epoch.
        /// </summary>
        [Key(0)]
        public ulong Active { get; set; }

        /// <summary>
        /// Stake inactive during the epoch.
        /// </summary>
        [Key(1)]
        public ulong Inactive { get; set; }

        /// <summary>
        /// The stake account's activation state, one of "active", "inactive", "activating", "deactivating".
        /// </summary>
        [Key(2)]
        public string State { get; set; }
    }
}