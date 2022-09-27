using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Enum
{
    public enum ResponseEnum
    {
        [Description("Successfull")]
        Success = 1,
        [Description("Request Failed")]
        Failure = 2,
        [Description("Already Registered")]
        AlreadyRegistered = 3,
        [Description("Already Applied")]
        Applied = 4,
        [Description("Record Not Found")]
        RecordNotFound = 5

        
    }
}
