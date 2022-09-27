using secureBridge_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.ViewModels
{
    public class OpportunityTypeVm
    {
        public bool IsHousing { get; set; }
        public bool IsJobs { get; set; }
        public bool IsEvent { get; set; }
        public bool IsFood { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
