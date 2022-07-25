using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;

namespace UnityCipher
{
    [TestFixture]
    public class RSAEncryptionTest
    {
        //For RSA encryption to generate public and private key.
        [TestCase(1024)]
        [TestCase(384)]
        public void GenrateKeyPairRegularTest(int keySize)
        {
            bool isGeneratable = (keySize % 8 == 0) && 384 <= keySize && keySize <= 16384;
            Assert.IsTrue(isGeneratable);
            KeyValuePair<string, string> publicPrivateKeypair1 = RSAEncryption.GenrateKeyPair(keySize);
            Assert.AreNotEqual(publicPrivateKeypair1.Key, publicPrivateKeypair1.Value);
            KeyValuePair<string, string> publicPrivateKeypair2 = RSAEncryption.GenrateKeyPair(keySize);
            Assert.AreNotEqual(publicPrivateKeypair1.Key, publicPrivateKeypair2.Key);
            Assert.AreNotEqual(publicPrivateKeypair1.Value, publicPrivateKeypair2.Value);
            Assert.DoesNotThrow(() => RSAEncryption.GenrateKeyPair(keySize));
        }

        //For RSA encryption to fail to generate public and private key.
        [TestCase(16385)]
        [TestCase(376)]
        [TestCase(1023)]
        public void GenrateKeyPairIrregularTest(int keySize)
        {
            bool isGeneratable = (keySize % 8 == 0) && 384 <= keySize && keySize <= 16384;
            Assert.IsFalse(isGeneratable);

            CryptographicException exception = Assert.Throws<CryptographicException>(() => RSAEncryption.GenrateKeyPair(keySize));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Specified key is not a valid size for this algorithm.");
        }

        //Test for encrypting strings
        [TestCase(384)]
        public void EncryptStringRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair = RSAEncryption.GenrateKeyPair(keySize);
            string plainString = Guid.NewGuid().ToString();
            string encripted = RSAEncryption.Encrypt(plainString, publicPrivateKeypair.Key);
            Assert.AreNotEqual(plainString, encripted);
        }

        //Test for encrypting binary
        [TestCase(392)]
        public void EncryptBinaryRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair = RSAEncryption.GenrateKeyPair(keySize);
            byte[] plainBinary = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            byte[] encripted = RSAEncryption.Encrypt(plainBinary, publicPrivateKeypair.Key);
            Assert.AreNotEqual(plainBinary, encripted);
        }

        //Test for decrypting strings
        [TestCase(400)]
        public void DecryptStringRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair = RSAEncryption.GenrateKeyPair(keySize);
            string plainString = Guid.NewGuid().ToString();
            string encripted = RSAEncryption.Encrypt(plainString, publicPrivateKeypair.Key);
            string decripted = RSAEncryption.Decrypt(encripted, publicPrivateKeypair.Value);
            Assert.AreEqual(plainString, decripted);
        }

        //Fail to decrypt strings
        [TestCase(1016)]
        public void DecryptStringIrregularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair1 = RSAEncryption.GenrateKeyPair(keySize);
            string plainString = Guid.NewGuid().ToString();
            string encripted = RSAEncryption.Encrypt(plainString, publicPrivateKeypair1.Key);
            // try to decrypt public key
            CryptographicException exception = Assert.Throws<CryptographicException>(() => RSAEncryption.Decrypt(encripted, publicPrivateKeypair1.Key));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Missing private key to decrypt value.");
        }

        //Test for decrypting binary
        [TestCase(408)]
        public void DecryptBinaryRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair = RSAEncryption.GenrateKeyPair(keySize);
            byte[] plainBinary = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            byte[] encripted = RSAEncryption.Encrypt(plainBinary, publicPrivateKeypair.Key);
            byte[] decripted = RSAEncryption.Decrypt(encripted, publicPrivateKeypair.Value);
            Assert.AreEqual(plainBinary, decripted);
        }

        //Fail to decrypt binary
        [TestCase(1032)]
        public void DecryptBinaryIrregularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair1 = RSAEncryption.GenrateKeyPair(keySize);
            byte[] plainBinary = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            byte[] encripted = RSAEncryption.Encrypt(plainBinary, publicPrivateKeypair1.Key);
            // try to decrypt public key
            CryptographicException exception = Assert.Throws<CryptographicException>(() => RSAEncryption.Decrypt(encripted, publicPrivateKeypair1.Key));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Missing private key to decrypt value.");
        }
    }
}