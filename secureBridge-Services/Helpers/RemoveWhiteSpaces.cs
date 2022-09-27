using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Helpers
{
    public static class RemoveWhiteSpaces
    {
        public static string RemoveSpaces(string input)
        {
            return new string(input.ToCharArray().Where(x => !Char.IsWhiteSpace(x)).ToArray());
        }
    }
}
