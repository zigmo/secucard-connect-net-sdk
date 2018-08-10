using System.Text;

namespace Zigmo.Secucard.Connect.NetCore.Net.Rest
{
    public static class RestHelp
    {
        public static byte[] ToUTF8Bytes(this string s)
        {
            return s == null ? null : Encoding.UTF8.GetBytes(s);
        }
    }
}