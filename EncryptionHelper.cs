using System;
using System.Security.Cryptography;
using System.Text;

public static class EncryptionHelper
{
    // 32‑byte key and 16‑byte IV
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF");
    private static readonly byte[] IV  = Encoding.UTF8.GetBytes("ABCDEF0123456789");

    public static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV  = IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] input = Encoding.UTF8.GetBytes(plainText);
            byte[] cipher = encryptor.TransformFinalBlock(input, 0, input.Length);
            return Convert.ToBase64String(cipher);
        }
    }

    public static string Decrypt(string cipherText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV  = IV;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] input = Convert.FromBase64String(cipherText);
            byte[] plain = decryptor.TransformFinalBlock(input, 0, input.Length);
            return Encoding.UTF8.GetString(plain);
        }
    }
}
