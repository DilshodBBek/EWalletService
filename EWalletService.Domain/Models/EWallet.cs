using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
        [Range(0, 101)]
        public double AmountOfMoney
        {
            get { return AmountOfMoney; }
            set
            {
                if (IsIdentified && value > 100)
                {
                    throw new ArgumentException("Money in e-wallet cannot be greater than 100 if user is identified.");
                }
                else if (!IsIdentified && value > 50)
                {
                    throw new ArgumentException("Money in e-wallet cannot be greater than 50 if user is not identified.");
                }
                else
                {
                    AmountOfMoney = value;
                }
            }
        }

        /// <summary>
        /// Owner of the E-Wallet
        /// </summary>
        [Column("user_id")]
        [JsonIgnore]
        [NotNull]
        public required IdentityUser User { get; set; }

        /// <summary>
        /// Ensures that account is Identified  or Unidentified 
        /// </summary>
        [Column("is_identified")]
        [DefaultValue(false)]
        public bool IsIdentified { get; set; } = false;
    }
}
