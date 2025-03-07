// ReSharper disable ClassNeverInstantiated.Global
using MessagePack;
using System.Collections.Generic;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Represents supply info.
    /// </summary>
    [MessagePackObject]
    public class Supply
    {
        /// <summary>
        /// Circulating supply in lamports.
        /// </summary>
        [Key(0)]
        public ulong Circulating { get; set; }

        /// <summary>
        /// Non-circulating supply in lamports.
        /// </summary>
        [Key(1)]
        public ulong NonCirculating { get; set; }

        /// <summary>
        /// A list of account addresses of non-circulating accounts, as strings.
        /// </summary>
        [Key(2)]
        public IList<string> NonCirculatingAccounts { get; set; }

        /// <summary>
        /// Total supply in lamports.
        /// </summary>
        [Key(3)]
        public ulong Total { get; set; }
    }
}