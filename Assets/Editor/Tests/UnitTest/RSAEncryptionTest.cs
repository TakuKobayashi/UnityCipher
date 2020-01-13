using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
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
            string planeString = Guid.NewGuid().ToString();
            string encripted = RSAEncryption.Encrypt(planeString, publicPrivateKeypair.Key);
            Assert.AreNotEqual(planeString, encripted);
        }

        //Test for encrypting binary
        [TestCase(392)]
        public void EncryptBinaryRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair = RSAEncryption.GenrateKeyPair(keySize);
            byte[] planeBinary = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            byte[] encripted = RSAEncryption.Encrypt(planeBinary, publicPrivateKeypair.Key);
            Assert.AreNotEqual(planeBinary, encripted);
        }

        //Test for decrypting strings
        [TestCase(400)]
        public void DecryptStringRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair = RSAEncryption.GenrateKeyPair(keySize);
            string planeString = Guid.NewGuid().ToString();
            string encripted = RSAEncryption.Encrypt(planeString, publicPrivateKeypair.Key);
            string decripted = RSAEncryption.Decrypt(encripted, publicPrivateKeypair.Value);
            Assert.AreEqual(planeString, decripted);
        }

        //Fail to decrypt strings
        [TestCase(1016)]
        public void DecryptStringIrregularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair1 = RSAEncryption.GenrateKeyPair(keySize);
            string planeString = Guid.NewGuid().ToString();
            string encripted = RSAEncryption.Encrypt(planeString, publicPrivateKeypair1.Key);
            KeyValuePair<string, string> publicPrivateKeypair2 = RSAEncryption.GenrateKeyPair(keySize);
            CryptographicException exception = Assert.Throws<CryptographicUnexpectedOperationException>(() => RSAEncryption.Decrypt(encripted, publicPrivateKeypair2.Value));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Error occurred while decoding PKCS1 padding.");
        }

        //Test for decrypting binary
        [TestCase(408)]
        public void DecryptBinaryRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair = RSAEncryption.GenrateKeyPair(keySize);
            byte[] planeBinary = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            byte[] encripted = RSAEncryption.Encrypt(planeBinary, publicPrivateKeypair.Key);
            byte[] decripted = RSAEncryption.Decrypt(encripted, publicPrivateKeypair.Value);
            Assert.AreEqual(planeBinary, decripted);
        }

        //Fail to decrypt binary
        [TestCase(1032)]
        public void DecryptBinaryIrregularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair1 = RSAEncryption.GenrateKeyPair(keySize);
            byte[] planeBinary = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            byte[] encripted = RSAEncryption.Encrypt(planeBinary, publicPrivateKeypair1.Key);
            KeyValuePair<string, string> publicPrivateKeypair2 = RSAEncryption.GenrateKeyPair(keySize);
            CryptographicException exception = Assert.Throws<CryptographicUnexpectedOperationException>(() => RSAEncryption.Decrypt(encripted, publicPrivateKeypair2.Value));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Error occurred while decoding PKCS1 padding.");
        }
    }
}