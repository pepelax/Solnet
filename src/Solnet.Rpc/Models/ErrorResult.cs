using MessagePack;
using System.Text.Json.Serialization;

namespace Solnet.Rpc.Models
{
    /// <summary>
    /// Holds an error result.
    /// </summary>
    [MessagePackObject]
    public class ErrorResult
    {
        /// <summary>
        /// The error string.
        /// </summary>
        [JsonPropertyName("err")]
        [Key(0)]
        public TransactionError Error { get; set; }
    }
}