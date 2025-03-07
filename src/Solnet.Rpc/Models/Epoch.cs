using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents information about the current epoch.
    /// </summary>
    [MessagePackObject]
    public class EpochInfo
    {
        /// <summary>
        /// The current slot.
        /// </summary>
        [Key(0)]
        public ulong AbsoluteSlot { get; set; }

        /// <summary>
        /// The current block height.
        /// </summary>
        [Key(1)]
        public ulong BlockHeight { get; set; }

        /// <summary>
        /// The current epoch.
        /// </summary>
        [Key(2)]
        public ulong Epoch { get; set; }

        /// <summary>
        /// The current slot relative to the start of the current epoch.
        /// </summary>
        [Key(3)]
        public ulong SlotIndex { get; set; }

        /// <summary>
        /// The number of slots in this epoch
        /// </summary>
        [Key(4)]
        public ulong SlotsInEpoch { get; set; }
    }

    /// <summary>
    /// Represents information about the epoch schedule.
    /// </summary>
    [MessagePackObject]
    public class EpochScheduleInfo
    {
        /// <summary>
        /// The maximum number of slots in each epoch.
        /// </summary>
        [Key(0)]
        public ulong SlotsPerEpoch { get; set; }

        /// <summary>
        /// The number of slots before beginning of an epoch to calculate a leader schedule for that epoch.
        /// </summary>
        [Key(1)]
        public ulong LeaderScheduleSlotOffset { get; set; }

        /// <summary>
        /// The first normal-length epoch.
        /// </summary>
        [Key(2)]
        public ulong FirstNormalEpoch { get; set; }

        /// <summary>
        /// The first normal-length slot.
        /// </summary>
        [Key(3)]
        public ulong FirstNormalSlot { get; set; }

        /// <summary>
        /// Whether epochs start short and grow.
        /// </summary>
        [Key(4)]
        public bool Warmup { get; set; }
    }
}