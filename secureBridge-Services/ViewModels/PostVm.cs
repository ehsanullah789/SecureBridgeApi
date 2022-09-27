using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.ViewModels
{
    public class PostVm
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile ImageFile { get; set; }
        public string PostComment { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
