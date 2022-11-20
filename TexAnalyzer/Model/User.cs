using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : EntityBase
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
