using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesPage : IServicesPage
    {
        private readonly LapStoreContext ctx;

        public ServicesPage(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var Page=GetById(id);
                ctx.Remove(Page);
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Page> GetAll()
        {
            try
            {
                return ctx.Pages.ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Page>();
            }
        }

        public Page GetById(int id)
        {
            try
            {
                var Page = ctx.Pages.FirstOrDefault(x=>x.Id==id);
                return Page;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Page();
            }
        }

        public bool Save(Page Page)
        {
            try
            {
                if(Page.Id==null||Page.Id==0)
                {
                    ctx.Pages.Add(Page);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldPage = GetById(Page.Id);
                    if(oldPage != null)
                    {
                        oldPage.Tittle = Page.Tittle;
                        oldPage.PageImg= Page.PageImg;
                        oldPage.Desc = Page.Desc;
                        ctx.Pages.Update(oldPage);
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
