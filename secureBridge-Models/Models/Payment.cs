using secureBridge_Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.Models
{
    public class Payment : CommonEntities
    {
        [ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public int PaymentMethod { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
