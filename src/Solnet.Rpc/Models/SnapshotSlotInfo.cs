using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// The highest snapshot slot info.
    /// </summary>
    [MessagePackObject]
    public class SnapshotSlotInfo
    {
        /// <summary>
        /// The highest full snapshot slot.
        /// </summary>
        [Key(0)]
        public ulong Full { get; set; }

        /// <summary>
        /// The highest incremental snapshot slot based on <see cref="Full"/>.
        /// </summary>
        [Key(1)]
        public ulong? Incremental { get; set; }
    }
}
