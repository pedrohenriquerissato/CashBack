using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Domain.Models;

namespace CashBack.Domain.Repositories
{
    public interface ILoginRepository
    {
        Task<object> Login(string email, string password);
    }
}
