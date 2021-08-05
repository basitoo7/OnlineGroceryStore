using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class LoggedInUser
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        
    }
}