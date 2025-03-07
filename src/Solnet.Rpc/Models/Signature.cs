using MessagePack;
using System.Text.Json.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the signature status information.
    /// </summary>
    [MessagePackObject]
    public class SignatureStatusInfo
    {
        /// <summary>
        /// The slot the transaction was processed in.
        /// </summary>
        [Key(0)]
        public ulong Slot { get; set; }

        /// <summary>
        /// The number of blocks since signature confirmation.
        /// </summary>
        [Key(1)]
        public ulong? Confirmations { get; set; }

        /// <summary>
        /// The error if the transaction failed, null if it succeeded.
        /// </summary>
        [JsonPropertyName("err")]
        [Key(2)]
        public TransactionError Error { get; set; }

        /// <summary>
        /// The transaction's cluster confirmation status, either "processed", "confirmed" or "finalized".
        /// </summary>
        [Key(3)]
        public string ConfirmationStatus { get; set; }

        /// <summary>
        /// Memo associated with the transaction, null if no memo is present.
        /// </summary>
        [Key(4)]
        public string Memo { get; set; }

        /// <summary>
        /// The transaction signature as base-58 encoded string.
        /// </summary>
        [Key(5)]
        public string Signature { get; set; }

        /// <summary>
        /// Estimated production time as Unix timestamp, null if not available.
        /// </summary>
        [Key(6)]
        public ulong? BlockTime { get; set; }
    }
}