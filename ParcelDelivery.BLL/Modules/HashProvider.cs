using System.Security.Cryptography;
using System.Text;

namespace ParcelDelivery.BLL.Modules
{
    public class HashProvider
    {
        public static string Hash(string value)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            return Encoding.Unicode.GetString(sha.ComputeHash(Encoding.Unicode.GetBytes(value)));
        }
    }
}
