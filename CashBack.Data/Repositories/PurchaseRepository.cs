using System.Collections.Generic;
using System.Linq;
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

        //TODO Implement pagination
        /// <summary>
        /// Retorna todas as compras sem cashback
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Purchase>> GetAllWithRetailerAsync()
        {
            return await Context.Purchases.Include(r => r.Retailer).ToListAsync();
        }

        /// <summary>
        /// Retorna uma compra específica
        /// </summary>
        /// <param name="id">Código da compra</param>
        /// <returns></returns>
        public async Task<Purchase> GetWithRetailerByIdAsync(int id)
        {
            return await Context.Purchases.Include(r => r.Retailer).SingleOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Retorna todas as compras de um(a) revendedor(a) pelo cpf
        /// </summary>
        /// <param name="retailerDocumentId">CPF do(a) revendedor(a)</param>
        /// <returns></returns>
        public async Task<IEnumerable<Purchase>> GetAllWithRetailerByRetailerDocumentId(string retailerDocumentId)
        {
            return await Context.Purchases.Include(r => r.Retailer)
                .Where(r => r.Retailer.DocumentId == retailerDocumentId).ToListAsync();
        }
    }
}
