using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.ViewModels
{
    public class PaymentVm
    {
        public Guid OrganizationId  { get; set; }
        public int PaymenMethod { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
