using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesCategory
    {
        public List<Category> GetAll();
        public Category GetById(int id);
        public bool Save(Category Category);  
        public bool Delete(int id);
        public int Count();
    }
}
