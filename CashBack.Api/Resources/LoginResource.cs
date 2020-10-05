using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CashBack.Api.Resources
{
    public class LoginResource
    {
        [Required(ErrorMessage = "Favor informar o email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Favor informar a senha")]
        [JsonPropertyName("senha")]
        public string Password { get; set; }
    }
}
