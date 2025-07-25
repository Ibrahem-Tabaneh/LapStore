using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesOs
    {
        public List<Os> GetAll();
        public Os GetById(int id);
        public bool Save(Os Os);  
        public bool Delete(int id); 
    }
}
