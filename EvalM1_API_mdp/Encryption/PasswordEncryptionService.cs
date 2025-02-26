using System.Security.Cryptography;
using System.Text;

public class PasswordEncryptionService
{
    private readonly string _aesKey = "YourAesKeyHere";  // Utilisez une clé secrète pour AES
    private readonly string _rsaPublicKey = "YourRsaPublicKeyHere";  // Utilisez une clé publique pour RSA
    private readonly string _rsaPrivateKey = "YourRsaPrivateKeyHere";  // Utilisez une clé privée pour RSA

    public string EncryptPassword(string password, string type)
    {
        if (type == "PRO")
        {
            return EncryptWithRSA(password);
        }
        else
        {
            return EncryptWithAES(password);
        }
    }

    private string EncryptWithAES(string password)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(_aesKey); // Clé AES
            aesAlg.IV = new byte[16]; // Initialisation à zéro pour simplification

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(password);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    private string EncryptWithRSA(string password)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportFromPem(_rsaPublicKey.ToCharArray());  // Importer la clé publique RSA

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(password);
            byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(encryptedData);
        }
    }
}
