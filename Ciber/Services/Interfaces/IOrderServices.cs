using Ciber.Model;

namespace Ciber.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<List<Order>> GetAllOrders();
    }
}
