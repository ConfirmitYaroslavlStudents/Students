using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3
{
    public class User
    {
        public int Permission { get; set; }
        
        public User ()
        {
            Permission = 0;
        }

        public User (int permission)
        {
            Permission = permission;
        }
    }
}
