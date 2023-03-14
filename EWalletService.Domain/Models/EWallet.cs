using Microsoft.AspNetCore.Identity;
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
        /// Ensures that account is Identified  or Unidentified 
        /// </summary>
        [Column("is_identified")]
        public bool IsIdentified { get; set; }

        /// <summary>
        /// Amount money of E-Wallet
        /// </summary>
        [Column("amount_money")]
        public double Amount { get; set; }

        [NotMapped]
        [Range(0, 100)]
        [JsonIgnore]
        public double AmountOfMoney
        {
            get { return Amount; }
            set
            {
                if (!IsIdentified)
                {
                    if (value > 50)
                    {
                        throw new ArgumentException("1Money in e-wallet cannot be greater than 50 if user is identified.");
                    }
                }
                Amount = value;
            }
        }

        /// <summary>
        /// Owner of the E-Wallet
        /// </summary>
        [Column("user_id")]
        [JsonIgnore]
        [NotNull]
        public IdentityUser User { get; set; }

    }
}
