using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesOrderItem
    {
        public List<OrderItem> GetAll();
        public List<OrderItem> GetAllById(int id);
        public OrderItem GetById(int id);
        public Task<bool> SaveAsync(List<OrderItem> orderItems, int orderId);  // ⬅️ تعديل هنا
        public bool Delete(int id); 
    }
}
