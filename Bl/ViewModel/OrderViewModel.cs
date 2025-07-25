using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public DateTime OrderDate { get; set;}
        public DateTime EndDate { get; set;}
        public decimal TotalPrice { get; set;}
        
    }
}
