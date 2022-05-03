using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Security
{
    public interface IEncrypter
    {
        string GetHash(string value, string salt);

        string GetSalt();

        bool ValidateHash(string hashed, string plain, string salt);
    }
}
