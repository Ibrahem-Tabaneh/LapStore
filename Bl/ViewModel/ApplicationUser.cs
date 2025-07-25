using Domin.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("Fname"))]
        public string Fname { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("Lname"))]
        public string Lname { get; set; }
    }
}
