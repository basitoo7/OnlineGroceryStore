using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class UserRequest
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }
    }
}