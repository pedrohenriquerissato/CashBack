using System.Collections.Generic;
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

        //TODO Implment pagination
        /// <summary>
        /// Retorna todos revendedores com suas compras
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Retailer>> GetAllWithPurchase()
        {
            return await Context.Retailers.Include(p => p.Purchases).ToListAsync();
        }

        /// <summary>
        /// Retorna um(a) revendedor(a) com suas compras pelo CPF
        /// </summary>
        /// <param name="documentId">CPF do(a) revendedor(a)</param>
        /// <returns></returns>
        public async Task<Retailer> GetWithPurchaseByDocumentIdAsync(string documentId)
        {
            return await Context.Retailers.Include(p => p.Purchases)
                .SingleOrDefaultAsync(r => r.DocumentId == documentId);
        }
    }
}
