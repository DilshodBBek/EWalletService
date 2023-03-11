using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWalletService.Application.Models
{
    public class ResponseCore<T> where T : class
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; } = true;

        [JsonProperty("errorMessage")]
        public string? ErrorMessage { get; set; }

        [JsonProperty("result")]
        public T? Result { get; set; }

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; } = 200;
    }
}
