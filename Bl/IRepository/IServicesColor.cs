using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesColor
    {
        public List<Color> GetAll();
        public Color GetById(int id);
        public bool Save(Color color);  
        public bool Delete(int id); 
    }
}
