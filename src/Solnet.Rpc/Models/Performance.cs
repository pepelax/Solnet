using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents a performance sample.
    /// </summary>
    [MessagePackObject]
    public class PerformanceSample
    {
        /// <summary>
        /// Slot in which sample was taken at.
        /// </summary>
        [Key(0)]
        public ulong Slot { get; set; }

        /// <summary>
        /// Number of transactions in sample.
        /// </summary>
        [Key(1)]
        public ulong NumTransactions { get; set; }

        /// <summary>
        /// Number of slots in sample
        /// </summary>
        [Key(2)]
        public ulong NumSlots { get; set; }

        /// <summary>
        /// Number of seconds in a sample window.
        /// </summary>
        [Key(3)]
        public int SamplePeriodSecs { get; set; }
    }
}