using System;
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

        //TODO Implement pagination
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

        /// <summary>
        /// Cria um(a) novo(a) revendedor(a)
        /// </summary>
        /// <param name="retailer"></param>
        /// <returns></returns>
        public async Task<Retailer> CreateRetailer(Retailer retailer)
        {
            var encryptedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(retailer.Password, BCrypt.Net.HashType.SHA512);

            retailer.Password = encryptedPassword;

            await Context.Retailers.AddAsync(retailer);
            await Context.SaveChangesAsync();
            return retailer;
        }

        /// <summary>
        /// Atualiza um(a) revendedor(a)
        /// </summary>
        /// <param name="retailer"></param>
        /// <returns></returns>
        public async Task<Retailer> UpdateRetailer(Retailer retailer)
        {
            Context.Retailers.Update(retailer);
            await Context.SaveChangesAsync();
            return retailer;
        }

        /// <summary>
        /// Remove fisicamente um(a) revendedor(a)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRetailer(Retailer retailer)
        {
            try
            {
                Context.Retailers.Remove(retailer);
                await Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
