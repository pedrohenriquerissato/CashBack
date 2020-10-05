using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CashBack.Domain
{
    public class CashbackPurchaseDTO
    {
        [JsonPropertyName("compraCodigo")]
        public int Id { get; set; }

        [JsonPropertyName("compraValor")]
        public decimal Amount { get; set; }

        [JsonPropertyName("compraData")]
        public string Date { get; set; }

        [JsonPropertyName("cashbackPorcentagem")]
        public decimal? CashBackPercent { get; set; }

        [JsonPropertyName("cashbackValor")]
        public decimal? CashBack { get; set; }

        [JsonPropertyName("compraStatus")]
        public string Status { get; set; }
    }
}
