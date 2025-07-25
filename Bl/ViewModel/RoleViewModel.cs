using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Domin.Resources;
using Microsoft.AspNetCore.Identity;


namespace Bl.ViewModel
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Roles = new List<IdentityRole>();
            NewRole=new NewRole();
        }
        public List<IdentityRole> Roles { get; set; }
        public NewRole NewRole { get; set; }
    }

    public class NewRole
    {
        [ValidateNever]
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("RoleName"))]
        public string RoleName { get; set; }
    }
}
