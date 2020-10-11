using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CashBack.Domain.Attributes;
using CashBack.Domain.Models;

namespace CashBack.Api.Resources
{
    public class PurchaseResource
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor informar o código do produto")]
        [JsonPropertyName("compraCodigo")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Favor informar o valor da compra")]
        // [Min(0.01)]
        [JsonPropertyName("compraValor")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Favor informar a data da compra")]
        [ValidDate]
        [JsonPropertyName("compraData")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Favor informar o CPF do revendedor")]
        [ValidDocumentId]
        [JsonPropertyName("revendedorCpf")]
        public string RetailerDocumentId { get; set; }

        [JsonIgnore]
        public PurchaseStatus Status { get; set; }

        [JsonIgnore]
        public Retailer Retailer { get; set; }
    }
}
