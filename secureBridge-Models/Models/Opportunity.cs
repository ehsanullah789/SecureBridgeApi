using secureBridge_Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.Models
{
    public class Opportunity : CommonEntities
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsHousing { get; set; }
        public bool IsJob { get; set; }
        public bool IsEvent { get; set; }
        public bool IsFood { get; set; }

        [ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

    }
}
