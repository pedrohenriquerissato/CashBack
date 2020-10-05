using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Data.Context;
using CashBack.Domain.Models;
using CashBack.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.Repositories
{
    public class RetailerRepository : Repository<Retailer>, IRetailerRepository
    {
        public RetailerRepository(CashBackContext context) 
            : base(context)
        { }

        public async Task<IEnumerable<Retailer>> GetAllWithPurchase()
        {
            return await Context.Retailers.Include(p => p.Purchases).ToListAsync();
        }

        public async Task<Retailer> GetWithPurchaseByDocumentIdAsync(string documentId)
        {
            return await Context.Retailers.Include(p => p.Purchases)
                .SingleOrDefaultAsync(r => r.DocumentId == documentId);
        }
    }
}
