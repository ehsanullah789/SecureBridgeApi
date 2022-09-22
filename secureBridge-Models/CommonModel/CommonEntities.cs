using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Models.CommonModel
{
    public class CommonEntities
    {
        public CommonEntities()
        {
            CreatedDateTime = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
