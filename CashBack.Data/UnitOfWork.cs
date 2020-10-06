using CashBack.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CashBack.Data.Context;
using CashBack.Data.Repositories;

namespace CashBack.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly CashBackContext _context;
        private PurchaseRepository _purchaseRepository;
        private RetailerRepository _retailerRepository;
        private LoginRepository _loginRepository;

        public UnitOfWork(CashBackContext context)
        {
            _context = context;
        }

        public IPurchaseRepository Purchases => _purchaseRepository = _purchaseRepository ?? new PurchaseRepository(_context);

        public IRetailerRepository Retailers => _retailerRepository = _retailerRepository ?? new RetailerRepository(_context);

        public ILoginRepository Logins => _loginRepository = _loginRepository ?? new LoginRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
