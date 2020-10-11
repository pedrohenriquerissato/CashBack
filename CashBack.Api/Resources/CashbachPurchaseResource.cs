using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CashBack.Domain.Attributes;
using CashBack.Domain.Models;
using Newtonsoft.Json;

namespace CashBack.Api.Resources
{
    public class CashbachPurchaseResource
    {
        [JsonPropertyName("compraCodigo")]
        public int ProductId { get; set; }

        [JsonPropertyName("compraValor")]
        public decimal Amount { get; set; }

        [JsonPropertyName("compraData")]
        public string Date { get; set; }

        [JsonPropertyName("cashbackPorcentagem")]
        public decimal? CashBackPercent { get; internal set; }

        [JsonPropertyName("cashbackValor")]
        public decimal? CashBack { get; internal set; }

        [JsonPropertyName("compraStatus")]
        public string Status
        {
            get; internal set;
        }
    }
}
