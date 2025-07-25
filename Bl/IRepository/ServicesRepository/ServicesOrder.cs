using Bl.Data;
using Bl.Migrations;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesOrder : IServicesOrder
    {
        private readonly LapStoreContext ctx;
        private readonly IServicesOrderItem servicesOrderItem;

        public ServicesOrder(LapStoreContext ctx, 
            IServicesOrderItem servicesOrderItem)
        {
            this.ctx = ctx;
            this.servicesOrderItem = servicesOrderItem;
        }
        public bool Delete(int id)
        {
            try
            {
                var Order = GetById(id);
                ctx.Orders.Remove(Order);
                ctx.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Order> GetAll(string userId)
        {
            try
            {
                return ctx.Orders.Where(x=>x.CustomerId==userId).ToList();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Order>();
            }
        }

        public Order GetById(int id)
        {
            try
            {
                var Order = ctx.Orders.FirstOrDefault(x => x.Id == id);
                return Order;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Order();
            }
        }

        public async Task<bool> Save(Order order, List<OrderItem> items)
        {
            try
            {
                ctx.Orders.Add(order);
                ctx.SaveChanges();

               await servicesOrderItem.SaveAsync(items,order.Id);

                return true;

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false ;
            }
            
        }
    }
}
