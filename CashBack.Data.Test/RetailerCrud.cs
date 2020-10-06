using System.Threading.Tasks;
using CashBack.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CashBack.Data.Test
{
    public class RetailerCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider serviceProvider;

        public RetailerCrud(DbTest dbTest)
        {
            serviceProvider = dbTest.ServiceProvider;
        }

        public async Task Is_Possible_Retailer_Crud()
        {
            using (var context = serviceProvider.GetService<CashBackContext>())
            {

            }
        }
    }
}
