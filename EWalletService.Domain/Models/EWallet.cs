using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EWalletService.Domain.Models
{
    public class EWallet
    {
        public class Wallet
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
            [Required]

            [ForeignKey("user_id_FK")]
            public UserAccount User { get; set; }
        }
    }
}
