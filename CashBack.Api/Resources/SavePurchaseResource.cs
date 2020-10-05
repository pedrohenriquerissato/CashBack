using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CashBack.Domain.Attributes;
using CashBack.Domain.Models;
using DataAnnotationsExtensions;

namespace CashBack.Api.Resources
{
    public class SavePurchaseResource
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor informar o código do produto")]
        [JsonPropertyName("compraCodigo")]
        [Range(1, int.MaxValue, ErrorMessage = "O código do produto deve estar entre 1 e 2147483647")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Favor informar o valor da compra")]
        [JsonPropertyName("compraValor")]
        [Min(0.01, ErrorMessage = "O valor mínimo da compra deve ser maior que zero")]
        [Max(10000.00, ErrorMessage = "O valor máximo da compra deve ser de R$ 10.000,00")]
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
