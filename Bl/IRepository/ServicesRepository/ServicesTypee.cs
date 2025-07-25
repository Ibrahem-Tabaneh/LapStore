using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesTypee : IServicesTypee
    {
        private readonly LapStoreContext ctx;

        public ServicesTypee(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var Typee=GetById(id);
                Typee.CurrentStauts = 0;
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Typee> GetAll()
        {
            try
            {
                return ctx.Typies.Where(x=>x.CurrentStauts==1).ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Typee>();
            }
        }

        public Typee GetById(int id)
        {
            try
            {
                var Typee = ctx.Typies.FirstOrDefault(x=>x.Id==id);
                return Typee;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Typee();
            }
        }

        public bool Save(Typee Typee)
        {
            try
            {
                if(Typee.Id==null||Typee.Id==0)
                {
                    Typee.CurrentStauts = 1;
                    ctx.Typies.Add(Typee);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldTypee = GetById(Typee.Id);
                    if(oldTypee != null)
                    {
                        oldTypee.TypeName = Typee.TypeName;
                        ctx.Typies.Update(oldTypee);
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
