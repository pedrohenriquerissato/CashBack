using CashBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CashBack.Domain;
using CashBack.Domain.Repositories;
using CashBack.Service.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;
using RestSharp;

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

            if (purchase.Retailer.DocumentId == "153.509.460-56" || purchase.Retailer.DocumentId == "15350946056")
            {
                purchase.Status = PurchaseStatus.Approved;
            }
            else
            {
                purchase.Status = PurchaseStatus.Validating;
            }

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

        public async Task<IEnumerable<CashbackPurchaseDTO>> GetPurchaseByRetailerDocumentId(string retailerDocumentId)
        {
            var purchases = await _unitOfWork.Purchases.GetAllWithRetailerByRetailerDocumentId(retailerDocumentId);

            var cashBack = GetCashBackPercent(purchases.AsQueryable());

            var cashbackPurchases = new List<CashbackPurchaseDTO>();
            foreach (var purchase in purchases)
            {
                var cashbackPercent = cashBack[new DateTime(purchase.Date.Year, purchase.Date.Month, 01)];

                var newCashbackDto = new CashbackPurchaseDTO
                {
                    CashBackPercent = cashbackPercent,
                    CashBack = purchase.Amount * cashbackPercent,
                    Status = purchase.Status.Value,
                    Id = purchase.Id,
                    Amount = purchase.Amount,
                    Date = purchase.Date.ToShortDateString()
                };

                cashbackPurchases.Add(newCashbackDto);

            }

            return cashbackPurchases.AsEnumerable();
        }

        public async Task UpdatePurchase(Purchase newPurchase)
        {
            await _unitOfWork.Purchases.UpdateASync(newPurchase);
        }

        private static Dictionary<DateTime, decimal> GetCashBackPercent(IQueryable<Purchase> purchases)
        {
            var cashBack = new Dictionary<DateTime, decimal>();

            if (purchases == null) return cashBack;

            cashBack = (from p in purchases
                        group p by new { month = p.Date.Month, year = p.Date.Year }
                    into c
                        select new { TimePeriod = new DateTime(c.Key.year, c.Key.month, 01), Amount = (decimal)(c.Sum(d => d.Amount) < 1000 ? 0.1 : c.Sum(d => d.Amount) > 1500 ? 0.2 : 0.15) }
                ).ToDictionary(pair => pair.TimePeriod, pair => pair.Amount);

            return cashBack;
        }

        public async Task<object> GetCashBackTotal(string documentId)
        {
            var _documentId = Regex.Replace(documentId, @"[^\d]", "");
            var url = string.Format("https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/v1/");

            var client = new RestClient(url);
            var request = new RestRequest("cashback")
                .AddParameter("cpf", _documentId);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("token", "ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm");

            var response = await client.GetAsync<CashBackTotalModel>(request);

            if (response.statusCode != 200) return response.body.message;

            return new { valorCashBack = response.body.credit};
        }

        internal class Body
        {
            public int credit { get; set; }
            public string message { get; set; }
        }

        internal class CashBackTotalModel
        {
            public int statusCode { get; set; }
            public Body body { get; set; }
        }
    }
}
