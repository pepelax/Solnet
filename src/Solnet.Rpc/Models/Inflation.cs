using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents inflation governor information.
    /// </summary>
    [MessagePackObject]
    public class InflationGovernor
    {
        /// <summary>
        /// The initial inflation percentage from time zero.
        /// </summary>
        [Key(0)]
        public decimal Initial { get; set; }

        /// <summary>
        /// The terminal inflation percentage.
        /// </summary>
        [Key(1)]
        public decimal Terminal { get; set; }

        /// <summary>
        /// The rate per year at which inflation is lowered.
        /// <remarks>Rate reduction is derived using the target slot time as per genesis config.</remarks>
        /// </summary>
        [Key(2)]
        public decimal Taper { get; set; }

        /// <summary>
        /// Percentage of total inflation allocated to the foundation.
        /// </summary>
        [Key(3)]
        public decimal Foundation { get; set; }

        /// <summary>
        /// Duration of foundation pool inflation in years.
        /// </summary>
        [Key(4)]
        public decimal FoundationTerm { get; set; }
    }

    /// <summary>
    /// Represents the inflation rate information.
    /// </summary>
    [MessagePackObject]
    public class InflationRate
    {
        /// <summary>
        /// Epoch for which these values are valid.
        /// </summary>
        [Key(0)]
        public decimal Epoch { get; set; }

        /// <summary>
        /// Percentage of total inflation allocated to the foundation.
        /// </summary>
        [Key(1)]
        public decimal Foundation { get; set; }

        /// <summary>
        /// Percentage of total inflation.
        /// </summary>
        [Key(2)]
        public decimal Total { get; set; }

        /// <summary>
        /// Percentage of total inflation allocated to validators.
        /// </summary>
        [Key(3)]
        public decimal Validator { get; set; }
    }

    /// <summary>
    /// Represents the inflation reward for a certain address.
    /// </summary>
    [MessagePackObject]
    public class InflationReward
    {
        /// <summary>
        /// Epoch for which a reward occurred.
        /// </summary>
        [Key(0)]
        public ulong Epoch { get; set; }

        /// <summary>
        /// The slot in which the rewards are effective.
        /// </summary>
        [Key(1)]
        public ulong EffectiveSlot { get; set; }

        /// <summary>
        /// The reward amount in lamports.
        /// </summary>
        [Key(2)]
        public ulong Amount { get; set; }

        /// <summary>
        /// Post balance of the account in lamports.
        /// </summary>
        [Key(3)]
        public ulong PostBalance { get; set; }
    }
}