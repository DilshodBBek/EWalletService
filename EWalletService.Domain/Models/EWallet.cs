using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

namespace EWalletService.Domain.Models
{
    public class EWallet
    {
        /// <summary>
        /// E-Wallet Id 
        /// </summary>
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Amount money of E-Wallet
        /// </summary>
        [Column("amount_money")]
        public int AmountOfMoney { get; set; }

        /// <summary>
        /// Owner of the E-Wallet
        /// </summary>
        [Column("user_id")]
        [JsonIgnore]
        [NotNull]
        public IdentityUser User { get; set; }

        /// <summary>
        /// Ensures that account is Identified  or Unidentified 
        /// </summary>
        [Column("is_identified")]
        [DefaultValue(false)]
        public bool IsIdentified { get; set; } = false;
    }
}
