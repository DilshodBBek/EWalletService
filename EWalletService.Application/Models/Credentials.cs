using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWalletService.Application.Models
{
    public class Credentials
    {
        /// <summary>
        /// Username for authorization
        /// </summary>
        [Required]
        public string Username { get; set; }= string.Empty;

        /// <summary>
        /// Username for authorization
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Set whether user is identified or unidentified
        /// </summary>
        public bool IsIdentified { get; set; }
    }
}
