using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.ViewModels
{
    public class OpportunityVm
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsHousing { get; set; }
        public bool IsJob { get; set; }
        public bool IsEvent { get; set; }
        public bool IsFood { get; set; }
    }
}
