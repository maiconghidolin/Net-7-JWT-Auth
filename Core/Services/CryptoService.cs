using Sodium;
using System.Text;

namespace Core.Services;

public class CryptoService
{

    public static string Encryption(string password)
    {
        byte[] data = Encoding.ASCII.GetBytes(password);
        data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
        return Encoding.ASCII.GetString(data);
    }

    public static bool CheckPassword(string password, string hash)
    {
        password = Encryption(password);
        return hash == password;
    }

}
