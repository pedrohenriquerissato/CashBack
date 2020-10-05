using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Domain.Models;

namespace CashBack.Service.Interfaces
{
    public interface IRetailerService
    {
        Task<IEnumerable<Retailer>> GetAllRetailers();
        Task<Retailer> GetRetailerByDocumentId(string documentId);
        Task<Retailer> CreateRetailer(Retailer retailer);
        Task UpdateRetailer(Retailer retailer);
        Task DeleteRetailer(Retailer retailer);
    }
}
