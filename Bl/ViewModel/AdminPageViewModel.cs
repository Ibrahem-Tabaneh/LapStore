using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class AdminPageViewModel
    {
        public int CountProduct { get; set; }
        public int CountCategory { get; set; }
        public int CountUsers { get; set; }
        public List<VwUser> users { get; set; }

    }
}
