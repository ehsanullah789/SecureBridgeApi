using secureBridge_Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.Models
{
    public class ApplyOpportunity : CommonEntities
    {
        //[ForeignKey("Organization")]
        //public Guid OrganizationId { get; set; }
        //public virtual Organization Organization { get; set; }

        [ForeignKey("Opportunity")]
        public Guid OpportunityId { get; set; }
        public virtual Opportunity Opportunity { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
