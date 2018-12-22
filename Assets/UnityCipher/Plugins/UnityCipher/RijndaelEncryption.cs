using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace UnityCipher{
    public class RijndaelEncryption
    {
        private static int BufferKeySize = 32;
        private static int BlockSize = 256;
        private static int KeySize = 256;

        /// <summary>
        /// <para>If you want to update the settings, you can update the settings.</para>
        /// <para>【argument1】buffer key size</para>
        /// <para>【argument2】block size</para>
        /// <para>【argument3】key size</para>
        /// </summary>
        public static void UpdateEncryptionKeySize(int bufferKeySize = 32, int blockSize = 256, int keySize = 256)
        {
            BufferKeySize = bufferKeySize;
            BlockSize = blockSize;
            KeySize = keySize;
        }

        /// <summary>
        /// <para>Standard Rijndael(AES) encrypt</para>
        /// <para>【argument1】plane text</para>
        /// <para>【argument2】password</para>
        /// <para>【return】Encrypted and converted to Base64 string</para>
        /// </summary>
        public static string Encrypt(string plane, string password)
        {
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(plane), password);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// <para>Standard Rijndael(AES) encrypt</para>
        /// <para>【argument1】plane binary</para>
        /// <para>【argument2】password</para>
        /// <para>【return】Encrypted binary</para>
        /// </summary>
        public static byte[] Encrypt(byte[] src, string password)
        {
            RijndaelManaged rij = SetupRijndaelManaged;

            // A pseudorandom number is newly generated based on the inputted password
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, BufferKeySize);
            // The missing parts are specified in advance to fill in 0 length
            byte[] salt = new byte[BufferKeySize];
            // Rfc2898DeriveBytes gets an internally generated salt
            salt = deriveBytes.Salt;
            // The 32-byte data extracted from the generated pseudorandom number is used as a password
            byte[] bufferKey = deriveBytes.GetBytes(BufferKeySize);

            rij.Key = bufferKey;
            rij.GenerateIV();

            using (ICryptoTransform encrypt = rij.CreateEncryptor(rij.Key, rij.IV))
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                // first 32 bytes of salt and second 32 bytes of IV for the first 64 bytes
                List<byte> compile = new List<byte>(salt);
                compile.AddRange(rij.IV);
                compile.AddRange(dest);
                return compile.ToArray();
            }
        }

        /// <summary>
        /// <para>Standard Rijndael(AES) decrypt</para>
        /// <para>【argument1】encrypted string</para>
        /// <para>【argument2】password</para>
        /// <para>【return】Decrypted string</para>
        /// </summary>
        public static string Decrypt(string encrtpted, string password)
        {
            byte[] decripted = Decrypt(Convert.FromBase64String(encrtpted), password);
            return Encoding.UTF8.GetString(decripted);
        }

        /// <summary>
        /// <para>Standard Rijndael(AES) decrypt</para>
        /// <para>【argument1】encrypted binary</para>
        /// <para>【argument2】password</para>
        /// <para>【return】Decrypted binary</para>
        /// </summary>
        public static byte[] Decrypt(byte[] src, string password)
        {
            RijndaelManaged rij = SetupRijndaelManaged;

            List<byte> compile = new List<byte>(src);

            // First 32 bytes are salt.
            List<byte> salt = compile.GetRange(0, BufferKeySize);
            // Second 32 bytes are IV.
            List<byte> iv = compile.GetRange(BufferKeySize, BufferKeySize);
            rij.IV = iv.ToArray();

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt.ToArray());
            byte[] bufferKey = deriveBytes.GetBytes(BufferKeySize);    // Convert 32 bytes of salt to password
            rij.Key = bufferKey;

            byte[] plain = compile.GetRange(BufferKeySize * 2, compile.Count - (BufferKeySize * 2)).ToArray();

            using (ICryptoTransform decrypt = rij.CreateDecryptor(rij.Key, rij.IV))
            {
                byte[] dest = decrypt.TransformFinalBlock(plain, 0, plain.Length);
                return dest;
            }
        }

        private static RijndaelManaged SetupRijndaelManaged
        {
            get
            {
                RijndaelManaged rij = new RijndaelManaged();
                rij.BlockSize = BlockSize;
                rij.KeySize = KeySize;
                rij.Mode = CipherMode.CBC;
                rij.Padding = PaddingMode.PKCS7;
                return rij;
            }
        }
    }
}