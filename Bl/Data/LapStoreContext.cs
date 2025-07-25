using Bl.ViewModel;
using Domin.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Data
{
    public class LapStoreContext:IdentityDbContext<ApplicationUser>
    {
        public LapStoreContext(DbContextOptions<LapStoreContext> options):base(options)
        {
            
        }

        

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Typee> Typies { get; set; }
        public DbSet<Os> Oss { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<VwProduct> VwProducts { get; set; }
        public DbSet<VwUser> VwUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<VwProduct>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwProducts");
            });

            builder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwUsers");
            });


        }







    }
}
