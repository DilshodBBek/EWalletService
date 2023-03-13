using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EWalletService.Domain.Models
{
    public class TransactionHistory
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        [Column("transaction_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Sender wallet id
        /// </summary>
        [Column("sender_wallet_id")]
        [Required]
        public required EWallet SenderWalletId { get; set; }

        /// <summary>
        /// Receiver Wallet Id
        /// </summary>
        [Column("receiver_wallet_id")]
        [Required]
        public required EWallet ReceiverWalletId { get; set; }

        /// <summary>
        /// Amount of Money for Transaction
        /// </summary>
        [Column("transaction_amount")]
        public double TransactionAmount { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; }
    }
}
