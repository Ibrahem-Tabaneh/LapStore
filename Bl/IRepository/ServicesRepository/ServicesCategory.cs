using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesCategory : IServicesCategory
    {
        private readonly LapStoreContext ctx;

        public ServicesCategory(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var Category=GetById(id);
                Category.CurrentStatus = 0;
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Category> GetAll()
        {
            try
            {
                return ctx.Categories.Where(x=>x.CurrentStatus==1).ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Category>();
            }
        }

        public Category GetById(int id)
        {
            try
            {
                var Category = ctx.Categories.FirstOrDefault(x=>x.Id==id);
                return Category;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Category();
            }
        }

        public bool Save(Category Category)
        {
            try
            {
                if(Category.Id==null||Category.Id==0)
                {
                    Category.CurrentStatus = 1;
                    ctx.Categories.Add(Category);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldCategory = GetById(Category.Id);
                    if(oldCategory != null)
                    {
                        oldCategory.CategoryName = Category.CategoryName;
                        ctx.Categories.Update(oldCategory);
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

        public int Count()
        {
            return ctx.Products.Where(x => x.CurrentStatus == 1).Count();
        }

    }
}
