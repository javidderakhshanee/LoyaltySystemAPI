using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LoyaltySystemApi.Infrastructure.Extensions;

public static class Extensions
{
    private static byte[] entropy = { 65, 34, 87, 33 };
    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        using var aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = IV;

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

        using var swEncrypt = new StreamWriter(csEncrypt);
        {
            swEncrypt.Write(plainText);
        }
        
        return  msEncrypt.ToArray();
    }
    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        string plaintext = null;

        using var aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = IV;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

        using var srDecrypt = new StreamReader(csDecrypt);
        {
            plaintext = srDecrypt.ReadToEnd();
        }

        return plaintext;
    }
    public static string Encrypt(this string original)
    {
        if (string.IsNullOrEmpty(original))
            return string.Empty;

        using var myAes = Aes.Create();

        byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

        return Encoding.UTF8.GetString(encrypted);
    }
    public static string Decrypt(this string encryptedText)
    {
        if (string.IsNullOrEmpty(encryptedText))
            return string.Empty;

        using var myAes = Aes.Create();

        var encryptedTextBytes = Encoding.UTF8.GetBytes(encryptedText);

        return DecryptStringFromBytes_Aes(encryptedTextBytes, myAes.Key, myAes.IV);
    }

    public static T Deserialized<T>(this string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }
    public static string Serialize(this object obj)
    {
        if (obj == null)
            return string.Empty;

        return JsonSerializer.Serialize(obj);
    }
    public static string Compress(this string text)
    {
        return text;
    }
}
