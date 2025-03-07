using MessagePack;
using System.Text.Json.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the account info and associated stake for all the voting accounts in the current bank.
    /// </summary>
    [MessagePackObject]
    public class VoteAccount
    {
        /// <summary>
        /// The root slot for this vote account.
        /// </summary>
        [Key(0)]
        public ulong RootSlot { get; set; }

        /// <summary>
        /// The vote account address, as a base-58 encoded string.
        /// </summary>
        [JsonPropertyName("votePubkey")]
        [Key(1)]
        public string VotePublicKey { get; set; }

        /// <summary>
        /// The validator identity, as a base-58 encoded string.
        /// </summary>
        [JsonPropertyName("nodePubkey")]
        [Key(2)]
        public string NodePublicKey { get; set; }

        /// <summary>
        /// The stake, in lamports, delegated to this vote account and active in this epoch.
        /// </summary>
        [Key(3)]
        public ulong ActivatedStake { get; set; }

        /// <summary>
        /// Whether the vote account is staked for this epoch.
        /// </summary>
        [Key(4)]
        public bool EpochVoteAccount { get; set; }

        /// <summary>
        /// Percentage of rewards payout owed to the vote account.
        /// </summary>
        [Key(5)]
        public decimal Commission { get; set; }

        /// <summary>
        /// Most recent slot voted on by this vote account.
        /// </summary>
        [Key(6)]
        public ulong LastVote { get; set; }

        /// <summary>
        /// History of how many credits earned by the end of the each epoch.
        /// <remarks>
        /// Each array contains [epoch, credits, previousCredits];
        /// </remarks>
        /// </summary>
        [Key(7)]
        public ulong[][] EpochCredits { get; set; }
    }

    /// <summary>
    /// Represents the vote accounts.
    /// </summary>
    [MessagePackObject]
    public class VoteAccounts
    {
        /// <summary>
        /// Current vote accounts.
        /// </summary>
        [Key(0)]
        public VoteAccount[] Current { get; set; }

        /// <summary>
        /// Delinquent vote accounts.
        /// </summary>
        [Key(1)]
        public VoteAccount[] Delinquent { get; set; }
    }
}