using Ciber.Model;
using Ciber.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ciber.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly CiberDbContext _context;

        public OrderServices(CiberDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
