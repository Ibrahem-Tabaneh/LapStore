using Bl.Data;
using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesUser : IServicesUser
    {
        private readonly LapStoreContext ctx;

        public ServicesUser(LapStoreContext ctx)
        {
            this.ctx = ctx;
        }
        public List<VwUser> GetUsers()
        {
            try
            {
                return ctx.VwUsers.ToList();

            }catch(Exception ex)
            {
                return new List<VwUser>();
            }
            
        }
    }
}
