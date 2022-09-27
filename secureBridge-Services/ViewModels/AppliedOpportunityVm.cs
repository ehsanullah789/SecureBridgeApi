using secureBridge_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.ViewModels
{
    public class AppliedOpportunityVm
    {
        public Guid OpportunityId { get; set; }
        public Guid UserId { get; set; }
    }
}
