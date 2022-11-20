using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHelper.Model
{
    public static class Global
    {
        static EntityContext context;

        public static EntityContext Context
        {
            get
            {
                if (context == null)
                    context = new EntityContext();
                return context;
            }
        }
    }
}
