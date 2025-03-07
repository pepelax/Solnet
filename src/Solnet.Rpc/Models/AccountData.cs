// ReSharper disable ClassNeverInstantiated.Global
using MessagePack;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents the account info for a given token account.
    /// </summary>
    [MessagePackObject]
    public class TokenAccountInfo : AccountInfoBase
    {
        /// <summary>
        /// The parsed token account data field.
        /// </summary>
        [Key(4)]
        public TokenAccountData Data { get; set; }
    }

    /// <summary>
    /// Represents the account info for a given token account.
    /// </summary>
    [MessagePackObject]
    public class TokenMintInfo : AccountInfoBase
    {
        /// <summary>
        /// The parsed token account data field.
        /// </summary>
        [Key(4)]
        public TokenMintData Data { get; set; }
    }

    /// <summary>
    /// Represents a Token Mint account data.
    /// </summary>
    [MessagePackObject]
    public class TokenMintData
    {
        /// <summary>
        /// The program responsible for the account data.
        /// </summary>
        [Key(0)]
        public string Program { get; set; }

        /// <summary>
        /// Account data space.
        /// </summary>
        [Key(1)]
        public ulong Space { get; set; }

        /// <summary>
        /// The parsed token mint data.
        /// </summary>
        [Key(2)]
        public ParsedTokenMintData Parsed { get; set; }
    }

    /// <summary>
    /// Represents the Token Mint parsed data, as formatted per SPL token program.
    /// </summary>
    [MessagePackObject]
    public class ParsedTokenMintData
    {
        /// <summary>
        /// Contains the details of the token mint.
        /// </summary>
        [Key(0)]
        public TokenMintInfoDetails Info { get; set; }

        /// <summary>
        /// The type of the account managed by the SPL token program.
        /// </summary>
        [Key(1)]
        public string Type { get; set; }
    }

    /// <summary>
    /// Represents a Token Mint account info as formatted per the SPL token program.
    /// </summary>
    [MessagePackObject]
    public class TokenMintInfoDetails
    {
        /// <summary>
        /// The freeze authority.
        /// </summary>
        [Key(0)]
        public string FreezeAuthority { get; set; }

        /// <summary>
        /// The mint authority.
        /// </summary>
        [Key(1)]
        public string MintAuthority { get; set; }

        /// <summary>
        /// The decimals cases to consider when converter to human readable token amounts.
        /// </summary>
        [Key(2)]
        public byte Decimals { get; set; }

        /// <summary>
        /// Is the mint account initialized?
        /// </summary>
        [Key(3)]
        public bool IsInitialized { get; set; }

        /// <summary>
        /// The current token supply.
        /// </summary>
        [Key(4)]
        public string Supply { get; set; }

        /// <summary>
        /// The current token supply parsed as ulong.
        /// </summary>
        [IgnoreMember]
        public ulong SupplyUlong => ulong.Parse(Supply);
    }

    /// <summary>
    /// Represents the details of the info field of a token account.
    /// </summary>
    [MessagePackObject]
    public class TokenAccountInfoDetails
    {
        /// <summary>
        /// The token balance data.
        /// </summary>
        [Key(0)]
        public TokenBalance TokenAmount { get; set; }

        /// <summary>
        /// A base-58 encoded public key of the delegate.
        /// </summary>
        [Key(1)]
        public string Delegate { get; set; }

        /// <summary>
        /// The token balance that has been delegated.
        /// </summary>
        [Key(2)]
        public TokenBalance DelegatedAmount { get; set; }

        /// <summary>
        /// The account's state.
        /// </summary>
        [Key(3)]
        public string State { get; set; }

        /// <summary>
        /// If the account is a native token account.
        /// </summary>
        [Key(4)]
        public bool IsNative { get; set; }

        /// <summary>
        /// A base-58 encoded public key of the token's mint.
        /// </summary>
        [Key(5)]
        public string Mint { get; set; }

        /// <summary>
        /// A base-58 encoded public key of the program this account as been assigned to.
        /// </summary>
        [Key(6)]
        public string Owner { get; set; }
    }

    /// <summary>
    /// Represents the parsed account data, as available by the program-specific state parser.
    /// </summary>
    [MessagePackObject]
    public class ParsedTokenAccountData
    {
        /// <summary>
        /// The type of account.
        /// </summary>
        [Key(0)]
        public string Type { get; set; }

        /// <summary>
        /// The token account info, containing account balances, delegation and ownership info.
        /// </summary>
        [Key(1)]
        public TokenAccountInfoDetails Info { get; set; }
    }

    /// <summary>
    /// Represents a token account's data.
    /// </summary>
    [MessagePackObject]
    public class TokenAccountData
    {
        /// <summary>
        /// The program responsible for the account data.
        /// </summary>
        [Key(0)]
        public string Program { get; set; }

        /// <summary>
        /// Account data space.
        /// </summary>
        [Key(1)]
        public ulong Space { get; set; }

        /// <summary>
        /// The parsed account data, as available by the program-specific state parser.
        /// </summary>
        [Key(2)]
        public ParsedTokenAccountData Parsed { get; set; }
    }

    /// <summary>
    /// Represents a large token account.
    /// </summary>
    [MessagePackObject]
    public class LargeTokenAccount : TokenBalance
    {
        /// <summary>
        /// The address of the token account.
        /// </summary>
        [Key(4)]
        public string Address { get; set; }
    }

    /// <summary>
    /// Represents a large account.
    /// </summary>
    [MessagePackObject]
    public class LargeAccount
    {
        /// <summary>
        /// The lamports balance of the account.
        /// </summary>
        [Key(0)]
        public ulong Lamports { get; set; }


        /// <summary>
        /// The address of the token account.
        /// </summary>
        [Key(1)]
        public string Address { get; set; }
    }

    /// <summary>
    /// Represents the token balance of an account.
    /// </summary>
    [MessagePackObject]
    public class TokenBalance
    {
        /// <summary>
        /// The raw token account balance without decimals.
        /// </summary>
        [Key(0)]
        public string Amount { get; set; }

        /// <summary>
        /// The number of base 10 digits to the right of the decimal place.
        /// </summary>
        [Key(1)]
        public int Decimals { get; set; }

        /// <summary>
        /// The token account balance, using mint-prescribed decimals. DEPRECATED.
        /// </summary>
        [Obsolete("UiAmount is deprecated, please use UiAmountString instead.")]
        [IgnoreMember]
        public decimal? UiAmount { get; set; }

        /// <summary>
        /// The token account balance as a string, using mint-prescribed decimals.
        /// </summary>
        [Key(2)]
        public string UiAmountString { get; set; }

        /// <summary>
        /// The token account balance as a ulong
        /// </summary>
        [Key(3)]
        public ulong AmountUlong => Convert.ToUInt64(Amount);

        /// <summary>
        /// The token account balance as a decimal
        /// </summary>
        [IgnoreMember]
        public decimal AmountDecimal => Convert.ToDecimal(UiAmountString, CultureInfo.InvariantCulture);

        /// <summary>
        /// The token account balance as a double
        /// </summary>
        [IgnoreMember]
        public double AmountDouble => Convert.ToDouble(UiAmountString, CultureInfo.InvariantCulture);
    }
}