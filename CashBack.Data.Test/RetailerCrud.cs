using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashBack.Data.Context;
using CashBack.Data.Repositories;
using CashBack.Domain.Models;
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

        [Fact(DisplayName = "Retailer CRUD")]
        [Trait("CRUD", nameof(Retailer))]
        public async Task Is_Possible_Retailer_Crud()
        {
            using (var context = serviceProvider.GetService<CashBackContext>())
            {
                var repository = new RetailerRepository(context);
                var retailer = new Retailer
                {
                    // Fake - Generated at: https://www.4devs.com.br/gerador_de_cpf
                    DocumentId = "715.244.760-80", 
                    Email = Faker.Internet.Email(), 
                    FullName = Faker.Name.FullName(),
                    Password = Faker.Lorem.GetFirstWord(),
                };

                var resultInsert = await repository.CreateRetailer(retailer);
                Assert.NotNull(resultInsert);
                Assert.Equal(retailer.Email, resultInsert.Email);
                Assert.True(retailer.Id == resultInsert.Id);
                Assert.True(resultInsert.Id != 0);

                retailer.FullName = Faker.Name.FullName();
                var resultUpdate = await repository.UpdateRetailer(retailer);
                Assert.NotNull(resultUpdate);
                Assert.Equal(retailer.Email, resultUpdate.Email);
                Assert.Equal(retailer.FullName, resultUpdate.FullName);

                var resultRead = await repository.GetWithPurchaseByDocumentIdAsync(resultInsert.DocumentId);
                Assert.NotNull(resultRead);
                Assert.True(retailer.Id == resultRead.Id);
                Assert.True(resultInsert.FullName == resultRead.FullName);

                var resultDelete = await repository.DeleteRetailer(resultInsert);
                Assert.True(resultDelete);
            }
        }
    }
}
