using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesProduct
    {
        public List<Product> GetAll();
        public List<VwProduct> GetAllProductData();
        public List<VwProduct> NewProductData();
        public List<VwProduct> DeliverProductData();
        public Product GetById(int id);
        public VwProduct GetVwProductById(int id);
        public bool Save(Product Product);  
        public bool Delete(int id); 
        public List<VwProduct> RealtedItems(int id);
        public int Count();
    }
}
