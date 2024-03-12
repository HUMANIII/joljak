using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class EnCryptAES : MonoBehaviour
{
    public static string EncryptAes(string textToEncrypt, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            byte[] saltBytes = GenerateRandomBytes(32);
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, 100);

            aesAlg.Key = pdb.GetBytes(16);

            aesAlg.GenerateIV();
                        
            ICryptoTransform encryptor = aesAlg.CreateEncryptor();
                        
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
                        
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
                        
            byte[] combinedData = new byte[saltBytes.Length + aesAlg.IV.Length + cipherBytes.Length];
            Array.Copy(saltBytes, 0, combinedData, 0, saltBytes.Length);
            Array.Copy(aesAlg.IV, 0, combinedData, saltBytes.Length, aesAlg.IV.Length);
            Array.Copy(cipherBytes, 0, combinedData, saltBytes.Length + aesAlg.IV.Length, cipherBytes.Length);
                                   
            return Convert.ToBase64String(combinedData); 
        }

    }

    public static string DecryptAes(string textToDecrypt, string key)
    {
        byte[] combinedData = Convert.FromBase64String(textToDecrypt);
                
        byte[] saltBytes = new byte[32];
        Array.Copy(combinedData, 0, saltBytes, 0, saltBytes.Length);
        
        byte[] ivAndCipherText = new byte[combinedData.Length - saltBytes.Length];
        Array.Copy(combinedData, saltBytes.Length, ivAndCipherText, 0, ivAndCipherText.Length);

        byte[] iv = new byte[16];
        Array.Copy(ivAndCipherText, 0, iv, 0, iv.Length);

        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, 100);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = pdb.GetBytes(16);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor();

            byte[] cipherText = new byte[ivAndCipherText.Length - iv.Length];
            Array.Copy(ivAndCipherText, iv.Length, cipherText, 0, cipherText.Length);

            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    private static byte[] GenerateRandomBytes(int length)
    {
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}
