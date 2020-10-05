using CashBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Domain.Repositories;
using CashBack.Service.Interfaces;

namespace CashBack.Service.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Purchase> CreatePurchase(Purchase purchase)
        {
            await _unitOfWork.Purchases.InsertAsync(purchase);
            await _unitOfWork.CommitAsync();
            return purchase;
        }

        public async Task DeletePurchase(Purchase purchase)
        {
            _unitOfWork.Purchases.DeleteAsync(purchase);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Purchase>> GetAllWithRetailer()
        {
            return await _unitOfWork.Purchases.GetAllWithRetailerAsync();
        }

        public async Task<Purchase> GetPurchaseById(int id)
        {
            return await _unitOfWork.Purchases.GetWithRetailerByIdAsync(id);
        }

        public async Task<IEnumerable<Purchase>> GetPurchaseByRetailerDocumentId(string retailerDocumentId)
        {
            return await _unitOfWork.Purchases.GetAllWithRetailerByRetailerDocumentId(retailerDocumentId);
        }

        public async Task UpdatePurchase(Purchase newPurchase)
        {
            await _unitOfWork.Purchases.UpdateASync(newPurchase);
        }
    }
}
