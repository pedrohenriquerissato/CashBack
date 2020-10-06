using System.Threading.Tasks;
using BCrypt.Net;
using CashBack.Data.Context;
using CashBack.Domain.Models;
using CashBack.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.Repositories
{
    class LoginRepository : Repository<Retailer>, ILoginRepository
    {
        public LoginRepository(CashBackContext context) : base(context)
        {
        }

        /// <summary>
        /// Realiza o login de um(a) revendedor(a)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<object> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;

            if (string.IsNullOrWhiteSpace(password)) return null;


            var retailer = await Context.Retailers.FirstOrDefaultAsync(r =>
                r.Email.ToLower().Equals(email.ToLower()));

            if (retailer == null) return null;

            return !BCrypt.Net.BCrypt.Verify(password, retailer.Password, true, HashType.SHA512) ? null : retailer;
        }
    }
}
