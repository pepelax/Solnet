// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
using MessagePack;
using System.Text.Json.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents a token account.
    /// </summary>
    [MessagePackObject]
    public class TokenAccount
    {
        /// <summary>
        /// The token account info.
        /// </summary>
        [Key(0)]
        public TokenAccountInfo Account { get; set; }

        /// <summary>
        /// A base-58 encoded public key representing the account's public key.
        /// </summary>
        [JsonPropertyName("pubkey")]
        [Key(1)]
        public string PublicKey { get; set; }
    }
}