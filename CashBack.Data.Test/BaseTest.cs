using System;
using CashBack.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CashBack.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    public class DbTest : IDisposable
    {
        private string databaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", "")}";

        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<CashBackContext>(
                options => options.UseSqlServer($"server=localhost; database={databaseName}; user id=sa; password=yourStrong(!)Password"),
                ServiceLifetime.Transient
                );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<CashBackContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<CashBackContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
