using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<object> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("Email não pode ser vazio");

            if (string.IsNullOrWhiteSpace(password)) throw new Exception("Senha não pode ser vazia");


            var retailer = await Context.Retailers.FirstOrDefaultAsync(r =>
                r.Email.ToLower().Equals(email.ToLower()));

            if (retailer == null) return "Email e/ou senha inválido";

            if (!BCrypt.Net.BCrypt.Verify(password, retailer.Password, true, HashType.SHA512))
            {
                return null;
            }

            return retailer;
        }
    }
}
