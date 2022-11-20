using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.UIElemetn
{
    public class LinkButton
    {
        public string Label { get; set; }

        public string Description { get; set; }
        public RelayCommand LinkCommand { get; set; }
    }
}
