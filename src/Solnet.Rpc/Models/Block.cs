using MessagePack;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the block info.
    /// </summary>
    [MessagePackObject]
    public class BlockInfo
    {
        /// <summary>
        /// Estimated block production time.
        /// </summary>
        [Key(0)]
        public long BlockTime { get; set; }

        /// <summary>
        /// A base-58 encoded public key representing the block hash.
        /// </summary>
        [Key(1)]
        public string Blockhash { get; set; }

        /// <summary>
        /// A base-58 encoded public key representing the block hash of this block's parent.
        /// <remarks>
        /// If the parent block is no longer available due to ledger cleanup, this field will return
        /// '11111111111111111111111111111111'
        /// </remarks>
        /// </summary>
        [Key(2)]
        public string PreviousBlockhash { get; set; }

        /// <summary>
        /// The slot index of this block's parent.
        /// </summary>
        [Key(3)]
        public ulong ParentSlot { get; set; }

        /// <summary>
        /// The number of blocks beneath this block.
        /// </summary>
        [Key(4)]
        public long? BlockHeight { get; set; }

        /// <summary>
        /// Max transaction version allowed
        /// </summary>
        [Key(5)]
        public int maxSupportedTransactionVersion { get; set; }

        /// <summary>
        /// The rewards for this given block.
        /// </summary>
        [Key(6)]
        public RewardInfo[] Rewards { get; set; }

        /// <summary>
        /// Collection of transactions and their metadata within this block.
        /// </summary>
        [Key(7)]
        public TransactionMetaInfo[] Transactions { get; set; }
    }

    /// <summary>
    /// Represents the block commitment info.
    /// </summary>
    [MessagePackObject]
    public class BlockCommitment
    {
        /// <summary>
        /// A list of values representing the amount of cluster stake in lamports that has
        /// voted onn the block at each depth from 0 to (max lockout history + 1).
        /// </summary>
        [Key(0)]
        public ulong[] Commitment { get; set; }

        /// <summary>
        /// Total active stake, in lamports, of the current epoch.
        /// </summary>
        [Key(1)]
        public ulong TotalStake { get; set; }
    }

    /// <summary>
    /// Represents the fee calculator info.
    /// </summary>
    [MessagePackObject]
    public class FeeCalculator
    {
        /// <summary>
        /// The amount, in lamports, to be paid per signature.
        /// </summary>
        [Key(0)]
        public ulong LamportsPerSignature { get; set; }
    }

    /// <summary>
    /// Represents the fee calculator info.
    /// </summary>
    [MessagePackObject]
    public class FeeCalculatorInfo
    {
        /// <summary>
        /// The fee calculator info.
        /// </summary>
        [Key(0)]
        public FeeCalculator FeeCalculator { get; set; }
    }

    /// <summary>
    /// Represents block hash info.
    /// </summary>
    [MessagePackObject]
    public class BlockHash
    {
        /// <summary>
        /// A base-58 encoded string representing the block hash.
        /// </summary>
        [Key(0)]
        public string Blockhash { get; set; }

        /// <summary>
        /// The fee calculator data.
        /// </summary>
        [Key(1)]
        public FeeCalculator FeeCalculator { get; set; }
    }

    /// <summary>
    /// Represents the latest block hash info.
    /// </summary>
    [MessagePackObject]
    public class LatestBlockHash
    {
        /// <summary>
        /// A base-58 encoded string representing the block hash.
        /// </summary>
        [Key(0)]
        public string Blockhash { get; set; }

        /// <summary>
        /// The last block height at which the blockhash will be valid.
        /// </summary>
        [Key(1)]
        public ulong LastValidBlockHeight { get; set; }
    }

    /// <summary>
    /// Represents the block notification.
    /// </summary>
    [MessagePackObject]
    public class BlockNotification
    {
        /// <summary>
        /// Block information.
        /// </summary>
        [Key(0)]
        public long Slot { get; set; }

        /// <summary>
        /// Block number.
        /// </summary>
        [Key(1)]
        public BlockInfo Block { get; set; }
    }

    /// <summary>
    /// Represents the loaded addresses in block info
    /// </summary>
    [MessagePackObject]
    public class LoadedAddresses
    {
        /// <summary>
        /// Writeable addresses
        /// </summary>
        [Key(0)]
        public string[] Writable { get; set; } = [];

        /// <summary>
        /// Readonly addresses
        /// </summary>
        [Key(1)]
        public string[] Readonly { get; set; } = [];
    }
}