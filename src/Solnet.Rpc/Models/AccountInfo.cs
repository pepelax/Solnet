// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
using MessagePack;
using Solnet.Rpc.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// The base class of the account info, to be subclassed for token a account info classes.
    /// </summary>
    [MessagePackObject]
    [Union(0, typeof(TokenAccountInfo))]
    [Union(1, typeof(TokenMintInfo))]
    [Union(2, typeof(AccountInfo))]
    public class AccountInfoBase
    {
        /// <summary>
        /// The lamports balance of the account.
        /// </summary>
        [Key(0)]
        public ulong Lamports { get; set; }

        /// <summary>
        /// The account owner.
        /// <remarks>
        /// This value could be another regular address or a program.
        /// </remarks>
        /// </summary>
        [Key(1)]
        public string Owner { get; set; }

        /// <summary>
        /// Indicates whether the account contains a program (and is strictly read-only).
        /// </summary>
        [Key(2)]
        public bool Executable { get; set; }

        /// <summary>
        /// The epoch at which the account will next owe rent.
        /// </summary>
        [Key(3)]
        public ulong RentEpoch { get; set; }
    }

    /// <summary>
    /// Represents the account info.
    /// </summary>
    [MessagePackObject]
    public class AccountInfo : AccountInfoBase
    {
        /// <summary>
        /// The actual account data.
        /// <remarks>
        /// This field should contain two values: first value is the data, the second one is the encoding - should always read base64.
        /// </remarks>
        /// </summary>
        [JsonConverter(typeof(AccountDataConverter))]
        [Key(4)]
        public List<string> Data { get; set; }
    }

    /// <summary>
    /// Represents the tuple account key and account data.
    /// </summary>
    [MessagePackObject]
    public class AccountKeyPair
    {
        /// <summary>
        /// The account info.
        /// </summary>
        [Key(0)]
        public AccountInfo Account { get; set; }

        /// <summary>
        /// A base-58 encoded public key representing the account's public key.
        /// </summary>
        [JsonPropertyName("pubkey")]
        [Key(1)]
        public string PublicKey { get; set; }
    }

}