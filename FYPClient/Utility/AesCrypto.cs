using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FYPClient.Utility
{
    class AesCrypto
    {
        int keySize;
        string key;

        public string Encrypt(string localPath)
        {
            
            
            try
            {
                // to be populated wiht the contents of the file
                //string original = "sdfsfsfsdfsdfsdfsdfsf";
                string original = File.ReadAllText(localPath);
                // Create a new instance of the RijndaelManaged
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {

                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    myRijndael.KeySize = 256;
                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptAes(original, myRijndael.Key, myRijndael.IV);

                    // Decrypt the bytes to a string.
                    string roundtrip = DecryptAes(encrypted, myRijndael.Key, myRijndael.IV);

                    //Display the original data and the decrypted data.
                    string cipherText = Encoding.UTF8.GetString(encrypted);
                    Debug.WriteLine("Original:   {0}", original);
                    Debug.WriteLine("Encrypted : " + cipherText);
                    Debug.WriteLine("Round Trip: {0}", roundtrip);
                   
                    System.IO.File.WriteAllText(@"C:\Users\Jaya\Documents\CloudBuffer", cipherText);
                    return @"C:\Users\Jaya\Documents\CloudBuffer";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return null;
            }
        }

        public string Decrypt()
        {
            return null;
        }

        public string DecryptAes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
        public byte[] EncryptAes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
    }
}
