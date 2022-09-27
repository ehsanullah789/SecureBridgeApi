using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Helpers
{
    public static class GlobalVariable
    {
        private readonly static string globalVariable = "https://localhost:7290";
        public static string GblVariable()
        {
            return globalVariable;
        }
    }
}
