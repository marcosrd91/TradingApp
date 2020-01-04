using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMOVIES.Models
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public virtual Info Info { get; set; }


        public ICollection<Operations> Operations { get; set; }

    }




}
