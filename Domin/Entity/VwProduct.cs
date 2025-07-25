using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class VwProduct
    {
        public int Id { get; set; }     
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductImg { get; set; }
        public string? Desc { get; set; }
        public int CurrentStatus { get; set; }
        public string HardDisk { get; set; }
        public string RamSize { get; set; }
        public string ScreenReselution { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public string OsName { get; set; }
        public string TypeName { get; set; }


    }
}
