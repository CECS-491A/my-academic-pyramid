using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Category
    {
        public Category(DatabaseContext _DbContext)
        {
            this._DbContext = _DbContext;
        }

        protected DatabaseContext _DbContext;

        public virtual ICollection<User> group { get; set; }

        public virtual ICollection<Claim> action { get; set; }
    }
}
