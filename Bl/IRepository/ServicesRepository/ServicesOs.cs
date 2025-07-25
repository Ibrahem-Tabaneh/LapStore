using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesOs : IServicesOs
    {
        private readonly LapStoreContext ctx;

        public ServicesOs(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var Os=GetById(id);
                Os.CurrentStatus = 0;
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Os> GetAll()
        {
            try
            {
                return ctx.Oss.Where(x=>x.CurrentStatus==1).ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Os>();
            }
        }

        public Os GetById(int id)
        {
            try
            {
                var Os = ctx.Oss.FirstOrDefault(x=>x.Id==id);
                return Os;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Os();
            }
        }

        public bool Save(Os Os)
        {
            try
            {
                if(Os.Id==null||Os.Id==0)
                {
                    Os.CurrentStatus = 1;
                    ctx.Oss.Add(Os);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldOs = GetById(Os.Id);
                    if(oldOs != null)
                    {
                        oldOs.OsName = Os.OsName;
                        ctx.Oss.Update(oldOs);
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
