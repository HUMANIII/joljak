using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

public struct Option
{
    public CipherMode cipherMode;
    public PaddingMode paddingMode;
    public int keySize;
    public int blockSize;
    public int IVSize;

    public Option(CipherMode cipher, PaddingMode padding, int kSize, int bSize, int ivSize)
    {
        cipherMode = cipher;
        paddingMode = padding;
        keySize = kSize;
        blockSize = bSize;
        IVSize = ivSize;
    }
}

public class EncryptRijndael
{
    private static Option option = 
        new Option(CipherMode.CBC, PaddingMode.PKCS7, 256, 128, 16);
    public static string Encrypt256(string textToEncrypt, string key)
    {
        using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
        {
            rijndaelCipher.Mode = option.cipherMode;
            rijndaelCipher.Padding = option.paddingMode;
            rijndaelCipher.KeySize = option.keySize;
            rijndaelCipher.BlockSize = option.blockSize;

            byte[] saltBytes = GenerateRandomBytes(32);
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, 1000);
            rijndaelCipher.Key = pdb.GetBytes(32);
            rijndaelCipher.IV = GenerateRandomBytes(option.IVSize); 

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

            byte[] combinedData = new byte[saltBytes.Length + rijndaelCipher.IV.Length + cipherBytes.Length];
            Array.Copy(saltBytes, 0, combinedData, 0, saltBytes.Length);
            Array.Copy(rijndaelCipher.IV, 0, combinedData, saltBytes.Length, rijndaelCipher.IV.Length);
            Array.Copy(cipherBytes, 0, combinedData, saltBytes.Length + rijndaelCipher.IV.Length, cipherBytes.Length);

            return Convert.ToBase64String(combinedData);
        }
    }

    public static string Decrypt256(string textToDecrypt, string key)
    {
        using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
        {
            rijndaelCipher.Mode = option.cipherMode;
            rijndaelCipher.Padding = option.paddingMode;
            rijndaelCipher.KeySize = option.keySize;
            rijndaelCipher.BlockSize = option.blockSize;

            byte[] combinedData = Convert.FromBase64String(textToDecrypt);

            byte[] saltBytes = new byte[32];
            Array.Copy(combinedData, 0, saltBytes, 0, saltBytes.Length);


            byte[] ivAndCipherText = new byte[combinedData.Length - saltBytes.Length];
            Array.Copy(combinedData, saltBytes.Length, ivAndCipherText, 0, ivAndCipherText.Length);

            byte[] iv = new byte[option.IVSize];
            Array.Copy(ivAndCipherText, 0, iv, 0, iv.Length);
            rijndaelCipher.IV = iv;

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, saltBytes, 1000);
            rijndaelCipher.Key = pdb.GetBytes(32);

            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();

            byte[] cipherText = new byte[ivAndCipherText.Length - iv.Length];
            Array.Copy(ivAndCipherText, iv.Length, cipherText, 0, cipherText.Length);

            byte[] plainText = transform.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(plainText);
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

