using CashBack.Domain.Models;
using CashBack.Domain.Repositories;
using CashBack.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashBack.Service.Services
{
    public class RetailerService : IRetailerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RetailerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Retailer> CreateRetailer(Retailer retailer)
        {
            var encryptedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(retailer.Password, BCrypt.Net.HashType.SHA512);

            retailer.Password = encryptedPassword;

            await _unitOfWork.Retailers.InsertAsync(retailer);
            await _unitOfWork.CommitAsync();
            return retailer;
        }

        public async Task DeleteRetailer(Retailer retailer)
        {
            _unitOfWork.Retailers.DeleteAsync(retailer);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Retailer>> GetAllRetailers()
        {
            return await _unitOfWork.Retailers.GetAllWithPurchase();
        }

        public async Task<Retailer> GetRetailerByDocumentId(string documentId)
        {
            return await _unitOfWork.Retailers.GetWithPurchaseByDocumentIdAsync(documentId);
        }

        public async Task UpdateRetailer(Retailer retailer)
        {
            await _unitOfWork.Retailers.UpdateASync(retailer);
            await _unitOfWork.CommitAsync();
        }
    }
}
