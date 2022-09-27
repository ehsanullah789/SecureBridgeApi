using secureBridge_Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.Models
{
    public class OpportunityTypes:CommonEntities
    {
        public bool IsHousing { get; set; }
        public bool IsJobs { get; set; }
        public bool IsEvent { get; set; }
        public bool IsFood { get; set; }

        [ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
