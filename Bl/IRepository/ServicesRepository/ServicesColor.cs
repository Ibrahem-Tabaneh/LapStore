using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesColor : IServicesColor
    {
        private readonly LapStoreContext ctx;

        public ServicesColor(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var color=GetById(id);
                color.CurrentStatus = 0;
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Color> GetAll()
        {
            try
            {
                return ctx.Colors.Where(x=>x.CurrentStatus==1).ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Color>();
            }
        }

        public Color GetById(int id)
        {
            try
            {
                var color = ctx.Colors.FirstOrDefault(x=>x.Id==id);
                return color;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Color();
            }
        }

        public bool Save(Color color)
        {
            try
            {
                if(color.Id==null||color.Id==0)
                {
                    color.CurrentStatus = 1;
                    ctx.Colors.Add(color);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldcolor = GetById(color.Id);
                    if(oldcolor != null)
                    {
                        oldcolor.ColorName = color.ColorName;
                        ctx.Colors.Update(oldcolor);
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
