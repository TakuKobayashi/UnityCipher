using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

public class RijndaelCipher{
	private const int BufferKeySize = 32;
	private const int BlockSize = 256;
	private const int KeySize = 256;

	/// <summary>
	/// <para>スタンダードなAES暗号による暗号化</para>
	/// <para>【第1引数】平文のバイナリ</para>
	/// <para>【第2引数】パスワード</para>
	/// <para>【return】暗号化したバイナリ</para>
	/// </summary>
	public static RijndaelEncripter Encrypt(byte[] src, string password){
		RijndaelManaged rij = SetupRijndaelManaged;

		//入力されたパスワードをベースに擬似乱数を新たに生成
		Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, BufferKeySize);
		// 足りない部分は0を埋めるために長さをあらかじめ指定
		byte[] salt = new byte[BufferKeySize];
		// Rfc2898DeriveBytesが内部生成したなソルトを取得
		salt = deriveBytes.Salt;
		// 生成した擬似乱数から32バイト切り出したデータをパスワードにする
		byte[] bufferKey = deriveBytes.GetBytes(BufferKeySize);

		rij.Key = bufferKey;
		rij.GenerateIV ();

		using (ICryptoTransform encrypt = rij.CreateEncryptor(rij.Key, rij.IV))
		{
			byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
            RijndaelEncripter encripter = new RijndaelEncripter(salt, rij.IV, dest);
			return encripter;
		}
	}

	//スタンダードなAES暗号による復号化
	/// <summary>
	/// <para>スタンダードなAES暗号による復号化</para>
	/// <para>【第1引数】暗号化されたバイナリ</para>
	/// <para>【第2引数】パスワード</para>
	/// <para>【return】復号化したバイナリ</para>
	/// </summary>
	public static byte[] Decrypt(RijndaelEncripter encripter, string password)
	{
		RijndaelManaged rij = SetupRijndaelManaged;

		rij.IV = encripter.IV;

        Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, encripter.Salt);
		byte[] bufferKey = deriveBytes.GetBytes(BufferKeySize);    // 32バイトのsaltを切り出してパスワードに変換
		rij.Key = bufferKey;

        byte[] encrypted = encripter.Encrypted;

		using (ICryptoTransform decrypt = rij.CreateDecryptor(rij.Key, rij.IV))
		{
			byte[] dest = decrypt.TransformFinalBlock(encrypted, 0, encrypted.Length);
			return dest;
		}
	}

    private static RijndaelManaged SetupRijndaelManaged{
        get{
			RijndaelManaged rij = new RijndaelManaged();
			rij.BlockSize = BlockSize;
			rij.KeySize = KeySize;
			rij.Mode = CipherMode.CBC;
			rij.Padding = PaddingMode.PKCS7;
            return rij;
        }
    }
}