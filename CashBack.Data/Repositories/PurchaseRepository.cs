using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashBack.Data.Context;
using CashBack.Domain.Models;
using CashBack.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.Repositories
{
    public class PurchaseRepository: Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(CashBackContext context) 
            : base(context)
        { }

        public async Task<IEnumerable<Purchase>> GetAllWithRetailerAsync()
        {
            return await Context.Purchases.Include(r => r.Retailer).ToListAsync();
        }

        public async Task<Purchase> GetWithRetailerByIdAsync(int id)
        {
            return await Context.Purchases.Include(r => r.Retailer).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Purchase>> GetAllWithRetailerByRetailerDocumentId(string retailerDocumentId)
        {
            return await Context.Purchases.Include(r => r.Retailer)
                .Where(r => r.Retailer.DocumentId == retailerDocumentId).ToListAsync();
        }

        private CashBackContext MyMusicDbContext
        {
            get { return Context as CashBackContext; }
        }
    }
}
