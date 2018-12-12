using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace e_Commerce.Core.Model.Entity
{
    public class User:EntityBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
        public string TCKN { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public virtual IEnumerable<UserAdress> UserAdress { get; set; }
    }
}
