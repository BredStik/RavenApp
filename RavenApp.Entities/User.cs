using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenApp.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Login { get; set; }
    }
}
