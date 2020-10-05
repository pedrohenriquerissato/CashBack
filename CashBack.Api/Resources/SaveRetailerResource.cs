using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CashBack.Domain.Attributes;

namespace CashBack.Api.Resources
{
    public class SaveRetailerResource
    {
        [Required(ErrorMessage = "O campo nome completo é obrigatório")]
        [JsonPropertyName("nomeCompleto")]
        public string FullName { get; set; }

        [ValidDocumentId]
        [JsonPropertyName("cpf")]
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string DocumentId { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        [JsonPropertyName("senha")]
        public string Password { get; set; }
    }
}
