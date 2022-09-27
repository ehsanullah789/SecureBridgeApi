using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secureBridge_Services.Services.EncryptionServices
{
    public class EncryptionService : IEncryptionService
    {
        public string PasswordEncode(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public string PasswordDecode(string input)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }
    }
}
