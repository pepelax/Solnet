// ReSharper disable ClassNeverInstantiated.Global

using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the slot info.
    /// </summary>
    [MessagePackObject]
    public class SlotInfo
    {
        /// <summary>
        /// The parent slot.
        /// </summary>
        [Key(0)]
        public int Parent { get; set; }

        /// <summary>
        /// The root as set by the validator.
        /// </summary>
        [Key(1)]
        public int Root { get; set; }

        /// <summary>
        /// The current slot.
        /// </summary>
        [Key(2)]
        public int Slot { get; set; }
    }
}