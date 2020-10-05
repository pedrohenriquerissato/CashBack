using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CashBack.Api.Resources
{
    public class RetailerResource
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("nomeCompleto")]
        public string FullName { get; set; }

        [JsonPropertyName("cpf")]
        public string DocumentId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
