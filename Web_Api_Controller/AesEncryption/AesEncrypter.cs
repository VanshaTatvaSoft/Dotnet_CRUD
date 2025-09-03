using System.Security.Cryptography;
using System.Text;

namespace Web_Api_Controller.AesEncryption;

public static class AesEncrypter
{
    public static string Encrypt(string data, string key)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.GenerateIV();

        using ICryptoTransform encryptor = aes.CreateEncryptor();
        using MemoryStream ms = new();
        ms.Write(aes.IV, 0, aes.IV.Length);

        using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
        using (StreamWriter sw = new(cs))
        {
            sw.Write(data);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string cipherText, string key)
    {
        byte[] fullCipher = Convert.FromBase64String(cipherText);
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);

        byte[] iv = new byte[aes.BlockSize / 8];
        byte[] cipher = new byte[fullCipher.Length - iv.Length];

        Array.Copy(fullCipher, iv, iv.Length);
        Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

        aes.IV = iv;

        using ICryptoTransform decryptor = aes.CreateDecryptor();
        using MemoryStream ms = new(cipher);
        using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }
}
