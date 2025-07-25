using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class HomePage
    {
        public HomePage()
        {
            VwProducts = new List<VwProduct>();
            Sliders = new List<Slider>();
            NewVwProducts = new List<VwProduct>();
            DeliveryVwProducts = new List<VwProduct>();
        }
        public List<Slider> Sliders { get; set; }
        public List<VwProduct> VwProducts { get; set; }
        public List<VwProduct> NewVwProducts { get; set; }
        public List<VwProduct> DeliveryVwProducts { get; set; }


    }
}
