using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesTypee
    {
        public List<Typee> GetAll();
        public Typee GetById(int id);
        public bool Save(Typee Typee);  
        public bool Delete(int id); 
    }
}
