using secureBridge_Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.Models
{
    public class Post : CommonEntities
    {
        public string PostComment { get; set; } = string.Empty;

        [ForeignKey("User")] 
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Reply> Replies { get; set; }
    }
}
