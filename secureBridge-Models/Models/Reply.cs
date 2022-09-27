using secureBridge_Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.Models
{
    public class Reply : CommonEntities
    {
        public string ReplyComment { get; set; } = string.Empty;

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Post")]
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
