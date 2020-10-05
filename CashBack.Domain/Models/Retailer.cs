using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CashBack.Domain.Attributes;

namespace CashBack.Domain.Models
{
    public class Retailer
    {
        public Retailer()
        {
            Purchases = new Collection<Purchase>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Nome completo é obrigatório")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "CPF é obrigatório")]

        [ValidDocumentId]
        public string DocumentId { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}