using Bl.Data;
using Domin.Entity;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesProduct : IServicesProduct
    {
        private readonly LapStoreContext ctx;

        public ServicesProduct(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Delete(int id)
        {
            try
            {
                var Product=GetById(id);
                Product.CurrentStatus = 0;
                ctx.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                return ctx.Products.Where(x=>x.CurrentStatus==1).ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Product>();
            }
        }
        public List<VwProduct> GetAllProductData()
        {
            try
            {
                return ctx.VwProducts.Where(x=>x.CurrentStatus==1).Take(12).ToList();
            }catch (Exception ex)
            {
                return new List<VwProduct>();
            }
        }
        public Product GetById(int id)
        {
            try
            {
                var Product = ctx.Products.FirstOrDefault(x=>x.Id==id);
                return Product;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Product();
            }
        }
        public VwProduct GetVwProductById(int id)
        {
            try
            {
                var Product = ctx.VwProducts.FirstOrDefault(x => x.Id == id);
                return Product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new VwProduct();
            }
        }

        public bool Save(Product Product)
        {
            try
            {
                if(Product.Id==null||Product.Id==0)
                {
                    Product.CurrentStatus = 1;
                    ctx.Products.Add(Product);
                    ctx.SaveChanges();
                }

                else
                {
                    var oldProduct = GetById(Product.Id);
                    if(oldProduct != null)
                    {
                        oldProduct.ProductName = Product.ProductName;
                        oldProduct.ProductImg= Product.ProductImg;
                        oldProduct.ProductPrice = Product.ProductPrice;
                        oldProduct.CategoryId = Product.CategoryId;
                        oldProduct.ColorId = Product.ColorId;
                        oldProduct.HardDisk = Product.HardDisk;
                        oldProduct.RamSize = Product.RamSize;
                        oldProduct.Desc = Product.Desc;
                        oldProduct.OsId = Product.OsId;
                        oldProduct.ScreenReselution = Product.ScreenReselution;
                        oldProduct.TypeId = Product.TypeId;
                      
       
                        ctx.Products.Update(oldProduct);
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
        public List<VwProduct> RealtedItems(int id)
        {
            try
            {
                var product = GetById(id);
                var products=ctx.VwProducts.Where(x=>x.ProductPrice<=product.ProductPrice+100 && x.ProductPrice>=product.ProductPrice-100).ToList();
                return products;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<VwProduct>();
            }
        }

        public int Count()
        {
            return ctx.Products.Where(x=>x.CurrentStatus==1).Count();
        }
        public List<VwProduct> NewProductData()
        {
            try
            {
                return ctx.VwProducts.Where(x=>x.CurrentStatus==1).OrderByDescending(x=>x.Id).Take(5).ToList();  
            }catch(Exception ex)
            {
                return new List<VwProduct>();
            }
        }
        public List<VwProduct> DeliverProductData()
        {
            try
            {
                return ctx.VwProducts.Where(x=>x.CurrentStatus==1).Take(3).ToList();
            }catch(Exception ex)
            {
                return new List<VwProduct>();
            }
        }
    }
}
