using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main (string[] args)
    {
        string data = "Sensitive Data";
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        // Generate RSA Keys
        using (RSA rsa = RSA.Create())
        {
            // Export the keys
            RSAParameters privateKey = rsa.ExportParameters(true);
            RSAParameters publicKey = rsa.ExportParameters(false);

            // Sign the data
            byte[] signature = SignData(dataBytes, privateKey);

            // Verify signature
            bool isValid = VerifyData(dataBytes, signature, publicKey);
            Console.WriteLine($"Signature valid: {isValid}");
        }
    }

    static byte[] SignData(byte[] data, RSAParameters privateKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(privateKey);
            return rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }

    static bool VerifyData(byte[] data, byte[] signature, RSAParameters publicKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);
            return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}