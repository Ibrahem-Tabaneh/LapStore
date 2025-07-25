using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesPage
    {
        public List<Page> GetAll();
        public Page GetById(int id);
        public bool Save(Page Page);  
        public bool Delete(int id); 
    }
}
