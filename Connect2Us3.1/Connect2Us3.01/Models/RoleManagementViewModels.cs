using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Connect2Us3._01.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public UserRoleViewModel()
        {
            Roles = new List<string>();
        }
    }

    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Current Roles")]
        public List<string> CurrentRoles { get; set; }
        
        [Display(Name = "Available Roles")]
        public List<SelectListItem> AvailableRoles { get; set; }
        
        [Display(Name = "Selected Roles")]
        public List<string> SelectedRoles { get; set; }

        public AssignRoleViewModel()
        {
            CurrentRoles = new List<string>();
            AvailableRoles = new List<SelectListItem>();
            SelectedRoles = new List<string>();
        }
    }
}