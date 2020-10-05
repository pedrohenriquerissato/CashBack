using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashBack.Service.Interfaces
{
    public interface ILoginService
    {
        Task<object> Login(string email, string password);
    }
}
