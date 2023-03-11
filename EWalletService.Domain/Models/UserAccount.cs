using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EWalletService.Domain.Models
{
    public class UserAccount : IdentityUser
    {
        /// <summary>
        /// Ensures that user is Identified  or Unidentified 
        /// </summary>
        [Column("identified")]
        public bool IsIdentified { get; set; }
    }
}
