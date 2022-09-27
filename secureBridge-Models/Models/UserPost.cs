using secureBridge_Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.Models
{
    public class UserPost : CommonEntities
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Post> Comments { get; set; }
    }
}
