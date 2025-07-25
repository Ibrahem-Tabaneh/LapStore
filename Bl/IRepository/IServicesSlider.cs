using Domin.Entity;

namespace Bl.IRepository
{
    public interface IServicesSlider
    {
        public List<Slider> GetAll();
        public Slider GetById(int id);
        public bool Save(Slider Slider);  
        public bool Delete(int id); 
    }
}
