using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.ViewModels
{
    public class ReplyVm
    {
        public string ReplyComment { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
