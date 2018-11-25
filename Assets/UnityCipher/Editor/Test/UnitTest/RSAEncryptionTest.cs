using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnityCipher
{
    [TestFixture]
    public class RSAEncryptionTest
    {
        [TestCase(1024)]
        public void GenrateKeyPairRegularTest(int keySize)
        {
            KeyValuePair<string, string> publicPrivateKeypair1 = RSAEncryption.GenrateKeyPair(keySize);
            Assert.AreNotEqual(publicPrivateKeypair1.Key, publicPrivateKeypair1.Value);
            UnityEngine.Debug.Log(publicPrivateKeypair1.Key.Length);
            UnityEngine.Debug.Log(publicPrivateKeypair1.Value.Length);
            KeyValuePair<string, string> publicPrivateKeypair2 = RSAEncryption.GenrateKeyPair(keySize);
            Assert.AreNotEqual(publicPrivateKeypair1.Key, publicPrivateKeypair2.Key);
            Assert.AreNotEqual(publicPrivateKeypair1.Value, publicPrivateKeypair2.Value);
        }

        [TestCase(1024)]
        public void GenrateKeyPairIrregularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void EncryptStringRegularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void EncryptStringIrregularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void EncryptBinaryRegularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void EncryptBinaryIrregularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void DecryptStringRegularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void DecryptStringIrregularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void DecryptBinaryRegularTest(int keySize)
        {
        }

        [TestCase(1024)]
        public void DecryptBinaryIrregularTest(int keySize)
        {
        }
    }
}