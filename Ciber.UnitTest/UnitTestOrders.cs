using Ciber.Model;
using Ciber.Services;
using Ciber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Ciber.UnitTest
{
    public class UnitTestOrders
    {
        public UnitTestOrders()
        {
        }

        [Fact]
        public async Task GetAllOrders()
        {
            // mock DB context using In memmory
            var options = new DbContextOptionsBuilder<CiberDbContext>()
            .UseInMemoryDatabase(databaseName: "CiberDatabase")
            .Options;

            using (var context = new CiberDbContext(options))
            {
                var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 100,
                    OrderDate = DateTime.Now,
                },
                new Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    ProductId = 2,
                    Amount = 100,
                    OrderDate = DateTime.Now,
                }
            };
                context.Orders.AddRange(orders);
                context.SaveChanges();
            }

            using (var context = new CiberDbContext(options))
            {
                OrderServices orderServices = new OrderServices(context);
                List<Order> orders = await orderServices.GetAllOrders();

                Assert.Equal(2, orders.Count);
            }
        }
    }
}