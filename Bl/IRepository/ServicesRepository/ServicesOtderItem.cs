using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesOrderItem : IServicesOrderItem
    {
        private readonly LapStoreContext ctx;

        public ServicesOrderItem(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var OrderItem=GetById(id);
                ctx.OrderItems.Remove(OrderItem);
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<OrderItem> GetAll()
        {
            try
            {
                return ctx.OrderItems.ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<OrderItem>();
            }
        }

        public OrderItem GetById(int id)
        {
            try
            {
                var OrderItem = ctx.OrderItems.FirstOrDefault(x=>x.Id==id);
                return OrderItem;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new OrderItem();
            }
        }

        public async Task<bool> SaveAsync(List<OrderItem> orderItems, int orderId)
        {
            try
            {
                foreach (var item in orderItems)
                {
                    item.OrderId = orderId;
                }

                ctx.OrderItems.AddRange(orderItems);
                await ctx.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<OrderItem> GetAllById(int id)
        {
            var items=ctx.OrderItems.Where(x=>x.OrderId==id).ToList();

            return items;
        }

    }
}
