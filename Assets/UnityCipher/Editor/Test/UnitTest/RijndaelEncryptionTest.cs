using System;
using NUnit.Framework;

namespace UnityCipher
{
    [TestFixture]
    public class RijndaelEncryptionTest
    {
        //Test for encrypting and decrypting strings
        [Test]
        public void EncryptDecryptStringTest()
        {
            string src = Guid.NewGuid().ToString();
            string passowrd = Guid.NewGuid().ToString();
            Assert.AreNotEqual(src, passowrd);

            // Encrypted string will be a string different from the original string
            string encrypted = RijndaelEncryption.Encrypt(src, passowrd);
            Assert.AreNotEqual(src, encrypted);

            //When decrypting with the same password as when encrypting the encrypted one, the same as the binary before encryption is obtained
            string decrypted = RijndaelEncryption.Decrypt(encrypted, passowrd);
            Assert.AreEqual(src, decrypted);
        }

        //Test for encrypting and decrypting binary
        [Test]
        public void EncryptDecryptBinaryTest()
        {
            byte[] binary = GenerateRandomBinary(32);
            string passowrd = Guid.NewGuid().ToString();
            byte[] encrypted = RijndaelEncryption.Encrypt(binary, passowrd);
            bool isEquals = binary.Length == encrypted.Length;
            // Encrypted binary will be a binary different from the original binary
            if (isEquals)
            {
                for (int i = 0; i < binary.Length; ++i)
                {
                    if (binary[i] != encrypted[i])
                    {
                        isEquals = false;
                        break;
                    }
                }
            }
            Assert.IsFalse(isEquals);
            
            //When decrypting with the same password as when encrypting the encrypted one, the same as the binary before encryption is obtained
            byte[] decrypted = RijndaelEncryption.Decrypt(encrypted, passowrd);
            Assert.AreEqual(binary.Length, decrypted.Length);
            for (int i = 0; i < binary.Length; ++i)
            {
                Assert.AreEqual(binary[i], decrypted[i]);
            }
        }

        // Test features of AES encryption
        // Test for encrypting with the same binary and the same password, but the cryptogram will not be the same
        [Test]
        public void RijndaelEncryptBinaryTest()
        {
            byte[] binary = GenerateRandomBinary(32);
            string passowrd = Guid.NewGuid().ToString();
            byte[] encrypted1 = RijndaelEncryption.Encrypt(binary, passowrd);
            byte[] encrypted2 = RijndaelEncryption.Encrypt(binary, passowrd);
            bool isEquals = encrypted1.Length == encrypted2.Length;
            // Encrypted binary will be a binary different from the original binary
            if (isEquals)
            {
                for (int i = 0; i < encrypted1.Length; ++i)
                {
                    if (encrypted1[i] != encrypted2[i])
                    {
                        isEquals = false;
                        break;
                    }
                }
            }
            Assert.IsFalse(isEquals);
        }

        // Test features of AES encryption
        // Test for encrypting with the same string and the same password, but the cryptogram will not be the same
        [Test]
        public void RijndaelEncryptStringTest()
        {
            string src = Guid.NewGuid().ToString();
            string passowrd = Guid.NewGuid().ToString();
            Assert.AreNotEqual(src, passowrd);

            string encrypted1 = RijndaelEncryption.Encrypt(src, passowrd);
            string encrypted2 = RijndaelEncryption.Encrypt(src, passowrd);
            Assert.AreNotEqual(encrypted1, encrypted2);
        }

        // Test features of AES encryption
        // When decrypting different cryptogram encrypted with the same binary and the same password respectively, it becomes the same binary.
        [Test]
        public void RijndaelDecryptBinaryTest()
        {
            byte[] binary = GenerateRandomBinary(32);
            string passowrd = Guid.NewGuid().ToString();
            byte[] encrypted1 = RijndaelEncryption.Encrypt(binary, passowrd);
            byte[] encrypted2 = RijndaelEncryption.Encrypt(binary, passowrd);

            byte[] decrypted1 = RijndaelEncryption.Decrypt(encrypted1, passowrd);
            byte[] decrypted2 = RijndaelEncryption.Decrypt(encrypted2, passowrd);
            Assert.AreEqual(decrypted1.Length, decrypted2.Length);
            for (int i = 0; i < decrypted1.Length; ++i)
            {
                Assert.AreEqual(decrypted1[i], decrypted2[i]);
            }
        }

        // Test features of AES encryption
        // When decrypting different cryptogram encrypted with the same binary and the same password respectively, it becomes the same string.
        [Test]
        public void RijndaelDecryptStringTest()
        {
            string src = Guid.NewGuid().ToString();
            string passowrd = Guid.NewGuid().ToString();

            string encrypted1 = RijndaelEncryption.Encrypt(src, passowrd);
            string encrypted2 = RijndaelEncryption.Encrypt(src, passowrd);

            string decrypted1 = RijndaelEncryption.Decrypt(encrypted1, passowrd);
            string decrypted2 = RijndaelEncryption.Decrypt(encrypted2, passowrd);
            Assert.AreEqual(decrypted1, decrypted2);
        }

        //Test for being able to update keysize, and encrypt and decrypt.
        [TestCase(16, 128, 128)]
        public void UpdateKeysAndEncrypting(int bufferKeySize, int blockSize, int keySize)
        {
            RijndaelEncryption.UpdateEncryptionKeySize(
                bufferKeySize: bufferKeySize,
                blockSize: blockSize,
                keySize: keySize
            );

            string src = Guid.NewGuid().ToString();
            string passowrd = Guid.NewGuid().ToString();
            Assert.AreNotEqual(src, passowrd);

            // Encrypted string will be a string different from the original string
            string encrypted = RijndaelEncryption.Encrypt(src, passowrd);
            Assert.AreNotEqual(src, encrypted);

            //When decrypting with the same password as when encrypting the encrypted one, the same as the binary before encryption is obtained
            string decrypted = RijndaelEncryption.Decrypt(encrypted, passowrd);
            Assert.AreEqual(src, decrypted);

            RijndaelEncryption.UpdateEncryptionKeySize(
                bufferKeySize: 32,
                blockSize: 256,
                keySize: 256
            );
        }

        private byte[] GenerateRandomBinary(int bufferSize)
        {
            byte[] binary = new byte[bufferSize];
            Random rand = new Random();
            for (int i = 0; i < bufferSize; ++i)
            {
                binary[i] = Convert.ToByte(rand.Next(byte.MinValue, byte.MaxValue + 1));
            }
            return binary;
        }
    }
}