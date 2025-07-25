using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesOrder
    {
        public List<Order> GetAll(string userId);
        public Order GetById(int id);
        public Task<bool> Save(Order order,List<OrderItem> items);  
        public bool Delete(int id); 
    }
}
