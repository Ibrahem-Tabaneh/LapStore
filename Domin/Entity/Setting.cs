using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class Setting
    {
        [Key]
        [ValidateNever] 
        public int Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("WebsiteName"))]
        public string WebsiteName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("WebsiteDescription"))]
        public string WebsiteDescription { get; set; }
        public string? Logo { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("Address"))]
        public string Address { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("Email"))]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("ValidEmail"))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("ContactNumber"))]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ValidNumber")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("InstgramLink"))]
        public string InstgramLink { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("TwitterLink"))]
        public string TwitterLink { get; set; }
    }
}
