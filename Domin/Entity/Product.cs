using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class Product
    {
        [Key]
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("ProductName"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MaxWord"))]
        [MinLength(2, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MinWord"))]
        public string ProductName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("ProductPrice"))]
        public decimal ProductPrice { get; set; }
        [ValidateNever]
        public string? ProductImg { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("HardDisk"))]
        public string HardDisk { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("RamSize"))]
        public string RamSize { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("ScreenReselution"))]
        public string ScreenReselution { get; set; }
        public string? Desc { get; set; }
        [ValidateNever]
        public int CurrentStatus { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        [ValidateNever]
        public Color Color { get; set; }
        public int OsId  { get; set; }
        [ForeignKey("OsId")]
        [ValidateNever]
        public Os Os { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        [ValidateNever]
        public Typee Type { get; set; }

    }
}
