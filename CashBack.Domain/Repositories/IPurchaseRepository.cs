using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Domain.Models;

namespace CashBack.Domain.Repositories
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetAllWithRetailerAsync();
        Task<Purchase> GetWithRetailerByIdAsync(int id);
        Task<IEnumerable<Purchase>> GetAllWithRetailerByRetailerDocumentId(string retailerDocumentId);
    }
}
