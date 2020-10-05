using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Domain.Models;

namespace CashBack.Domain.Repositories
{
    public interface IRetailerRepository : IRepository<Retailer>
    {
        Task<IEnumerable<Retailer>> GetAllWithPurchase();
        Task<Retailer> GetWithPurchaseByDocumentIdAsync(string id);
    }
}
