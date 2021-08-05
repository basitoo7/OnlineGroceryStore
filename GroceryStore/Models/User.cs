using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Designation { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long CatUserRoleId { get; set; }
    }
}