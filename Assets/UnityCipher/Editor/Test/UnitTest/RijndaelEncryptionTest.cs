using System;
using NUnit.Framework;

namespace UnityCipher
{
    [TestFixture]
    public class RijndaelEncryptionTest
    {
        //文字列を暗号、復号するテスト
        [Test]
        public void EncryptDecryptStringTest()
        {
            string src = Guid.NewGuid().ToString();
            string passowrd = Guid.NewGuid().ToString();
            Assert.AreNotEqual(src, passowrd);

            // 暗号化かけたら元の文字列とは異なる文字列となる
            string encrypted = RijndaelEncryption.Encrypt(src, passowrd);
            Assert.AreNotEqual(src, encrypted);

            // 暗号化されたものを暗号化した時と同じパスワードで復号すると暗号化する前の文字列と同じものが得られる
            string decrypted = RijndaelEncryption.Decrypt(encrypted, passowrd);
            Assert.AreEqual(src, decrypted);
        }

        //バイナリを暗号、復号するテスト
        [Test]
        public void EncryptDecryptBinaryTest()
        {
            byte[] binary = GenerateRandomBinary(32);
            string passowrd = Guid.NewGuid().ToString();
            byte[] encrypted = RijndaelEncryption.Encrypt(binary, passowrd);
            bool isEquals = binary.Length == encrypted.Length;
            // 暗号化かけたら元のバイナリとは異なるバイナリとなる
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

            // 暗号化されたものを暗号化した時と同じパスワードで復号すると暗号化する前のバイナリと同じものが得られる
            byte[] decrypted = RijndaelEncryption.Decrypt(encrypted, passowrd);
            Assert.AreEqual(binary.Length, decrypted.Length);
            for (int i = 0; i < binary.Length; ++i)
            {
                Assert.AreEqual(binary[i], decrypted[i]);
            }
        }

        // AES暗号の特徴をテストする
        // 同じバイナリ、同じパスワードで暗号化しても暗号文は同じにはならない
        [Test]
        public void RijndaelEncryptBinaryTest()
        {
            byte[] binary = GenerateRandomBinary(32);
            string passowrd = Guid.NewGuid().ToString();
            byte[] encrypted1 = RijndaelEncryption.Encrypt(binary, passowrd);
            byte[] encrypted2 = RijndaelEncryption.Encrypt(binary, passowrd);
            bool isEquals = encrypted1.Length == encrypted2.Length;
            // 暗号化かけたら元のバイナリとは異なるバイナリとなる
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

        // AES暗号の特徴をテストする
        // 同じバイナリ、同じパスワードで暗号化しても暗号文は同じにはならない
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

        // AES暗号の特徴をテストする
        // 同じバイナリ、同じパスワードで暗号化した異なる暗号文をそれぞれ復号すると同じバイナリになる
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

        // AES暗号の特徴をテストする
        // 同じ文字列、同じパスワードで暗号化した異なる暗号文をそれぞれ復号すると同じ文字列になる
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