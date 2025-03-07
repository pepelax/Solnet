using MessagePack;
using System.Collections.Generic;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Holds the block production information.
    /// </summary>
    [MessagePackObject]
    public class BlockProductionInfo
    {
        /// <summary>
        /// The block production as a map from the validator to a list 
        /// of the number of leader slots and number of blocks produced
        /// </summary>
        [Key(0)]
        public Dictionary<string, List<int>> ByIdentity { get; set; }

        /// <summary>
        /// The block production range by slots.
        /// </summary>
        [Key(1)]
        public SlotRange Range { get; set; }
    }

    /// <summary>
    /// Represents a slot range.
    /// </summary>
    [MessagePackObject]
    public class SlotRange
    {
        /// <summary>
        /// The first slot of the range (inclusive).
        /// </summary>
        [Key(0)]
        public ulong FirstSlot { get; set; }

        /// <summary>
        /// The last slot of the range (inclusive).
        /// </summary>
        [Key(1)]
        public ulong LastSlot { get; set; }

    }
}