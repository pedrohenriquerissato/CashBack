using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CashBack.Domain.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor informar o código do produto")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Favor informar o valor da compra")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Favor informar a data da compra")]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public PurchaseStatus Status { get; set; }

        [JsonIgnore]
        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; }

        [NotMapped]
        public decimal? CashBackPercent { get; set; }

        [NotMapped]
        public decimal? CashBack { get; set; }
    }

    public class PurchaseStatus
    {
        public PurchaseStatus() { }
        private PurchaseStatus(string value) { Value = value; }

        public string Value { get; set; }

        public static PurchaseStatus Approved => new PurchaseStatus("Aprovado");
        public static PurchaseStatus Validating => new PurchaseStatus("Em validação");
    }
}