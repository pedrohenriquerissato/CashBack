using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Domain.Models;

namespace CashBack.Service.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetAllWithRetailer();
        Task<Purchase> GetPurchaseById(int id);
        Task<IEnumerable<Purchase>> GetPurchaseByRetailerDocumentId(string retailerDocumentId);
        Task<Purchase> CreatePurchase(Purchase purchase);
        Task UpdatePurchase(Purchase purchase);
        Task DeletePurchase(Purchase purchase);
    }
}
