using System.Security.Cryptography;
using System.Text;

namespace Divisima.UI.Tools
{
    public class GeneralTool
    {
        public static string getMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static string getURL(string _text)
        {
            return _text.ToLower().Replace(" ", "-").Replace("ş", "s").Replace("ç", "c").Replace("ğ", "g").Replace("ö", "o").Replace("ü", "u").Replace("ı", "i").Replace("&", "-").Replace("?", "-");
        }
    }
}
