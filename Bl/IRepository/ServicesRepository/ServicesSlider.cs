using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesSlider : IServicesSlider
    {
        private readonly LapStoreContext ctx;

        public ServicesSlider(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var Slider=GetById(id);
                Slider.CurrentStatus = 0;
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Slider> GetAll()
        {
            try
            {
                return ctx.Sliders.Where(x=>x.CurrentStatus==1).ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Slider>();
            }
        }

        public Slider GetById(int id)
        {
            try
            {
                var Slider = ctx.Sliders.FirstOrDefault(x=>x.Id==id);
                return Slider;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Slider();
            }
        }

        public bool Save(Slider Slider)
        {
            try
            {
                if(Slider.Id==null||Slider.Id==0)
                {
                    Slider.CurrentStatus = 1;
                    ctx.Sliders.Add(Slider);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldSlider = GetById(Slider.Id);
                    if(oldSlider != null)
                    {
                        oldSlider.SliderName = Slider.SliderName;
                        oldSlider.SliderImg= Slider.SliderImg;
                        ctx.Sliders.Update(oldSlider);
                        ctx.SaveChanges() ;
                    }
                    
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
