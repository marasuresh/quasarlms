using System;
using System.Collections.Generic;
using System.Text;

static class HashPasswordCheck
{
    public static bool CheckPassword(string password, string hashEncoded)
    {
        string[] hashSaltParts = hashEncoded.Split(' ');

        Guid configHash = new Guid(hashSaltParts[0]);
        Guid configSalt = new Guid(hashSaltParts[1]);

        System.Security.Cryptography.PasswordDeriveBytes checkAdminHasher = new System.Security.Cryptography.PasswordDeriveBytes(
            Encoding.UTF8.GetBytes(password),
            configSalt.ToByteArray());

        Guid passwordHash = new Guid(checkAdminHasher.GetBytes(configHash.ToByteArray().Length));

        return passwordHash == configHash;
    }
}