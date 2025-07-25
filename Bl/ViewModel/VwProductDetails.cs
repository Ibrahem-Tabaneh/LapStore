using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class VwProductDetails
    {
        public VwProductDetails()
        {
            RelatedItems = new List<VwProduct>();
        }
        public VwProduct Product { get; set; }
        public List<VwProduct> RelatedItems { get; set; }
    }
}
