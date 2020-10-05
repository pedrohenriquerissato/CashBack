using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CashBack.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRetailerRepository Retailers { get; }
        IPurchaseRepository Purchases { get; }
        ILoginRepository Logins { get; }
        Task<int> CommitAsync();
    }
}
