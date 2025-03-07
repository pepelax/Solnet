using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the fee rate governor.
    /// </summary>
    [MessagePackObject]
    public class FeeRateGovernor
    {
        /// <summary>
        /// Percentage of fees collected to be destroyed.
        /// </summary>
        [Key(0)]
        public decimal BurnPercent { get; set; }

        /// <summary>
        /// Highest value LamportsPerSignature can attain for the next slot.
        /// </summary>
        [Key(1)]
        public ulong MaxLamportsPerSignature { get; set; }

        /// <summary>
        /// Smallest value LamportsPerSignature can attain for the next slot.
        /// </summary>
        [Key(2)]
        public ulong MinLamportsPerSignature { get; set; }

        /// <summary>
        /// Desired fee rate for the cluster.
        /// </summary>
        [Key(3)]
        public ulong TargetLamportsPerSignature { get; set; }

        /// <summary>
        /// Desired signature rate for the cluster.
        /// </summary>
        [Key(4)]
        public ulong TargetSignaturesPerSlot { get; set; }
    }

    /// <summary>
    /// Represents the fee rate governor info.
    /// </summary>
    [MessagePackObject]
    public class FeeRateGovernorInfo
    {
        /// <summary>
        /// The fee rate governor.
        /// </summary>
        [Key(0)]
        public FeeRateGovernor FeeRateGovernor { get; set; }
    }

    /// <summary>
    /// Represents information about the fees.
    /// </summary>
    [MessagePackObject]
    public class FeesInfo
    {
        /// <summary>
        /// A block hash as base-58 encoded string.
        /// </summary>
        [Key(0)]
        public string Blockhash { get; set; }

        /// <summary>
        /// The fee calculator for this block hash.
        /// </summary>
        [Key(1)]
        public FeeCalculator FeeCalculator { get; set; }

        /// <summary>
        /// DEPRECATED - this value is inaccurate and should not be relied upon
        /// </summary>
        [Key(2)]
        public ulong LastValidSlot { get; set; }

        /// <summary>
        /// Last block height at which a blockhash will be valid.
        /// </summary>
        [Key(3)]
        public ulong LastValidBlockHeight { get; set; }
    }
}