using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Accounts
    {

        public string username { get; set; }
        public string password { get; set; }
        public List<Shared_Users> shared_user { get; set; }
    }
}
