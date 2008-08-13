using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Security.Cryptography;

public static class HashServices
{
    static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

    public static Guid CalcPasswordHash(string password, Guid salt)
    {
        byte[] saltBytes = salt.ToByteArray();

        PasswordDeriveBytes hasher = new PasswordDeriveBytes(
            Encoding.UTF8.GetBytes(password),
            saltBytes);

        return new Guid(hasher.GetBytes(saltBytes.Length));
    }

    public static void GeneratePasswordHash(string password, out Guid hash, out Guid salt)
    {
        byte[] saltBytes = Guid.Empty.ToByteArray();
        lock( rng )
        {
            rng.GetBytes(saltBytes);
        }

        salt = new Guid(saltBytes);

        PasswordDeriveBytes hasher = new PasswordDeriveBytes(
            Encoding.UTF8.GetBytes(password),
            saltBytes);

        hash = new Guid(hasher.GetBytes(saltBytes.Length));
    }
}
