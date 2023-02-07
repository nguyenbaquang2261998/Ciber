using Ciber.Model;
using Ciber.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciber.UnitTest.Mocks
{
    public static class OrderServiceMock
    {
        public static Mock<IOrderServices> Gets()
        {
            var orderService = new Mock<IOrderServices>();

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

            orderService.Setup(x => x.GetAllOrders()).ReturnsAsync(orders);

            return orderService;
        }
    }
}
