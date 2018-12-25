# UnityCipher
This is Cipher Libraries in Unity, include the AESCipher and RSACipher.

# What is UnityCipher?
UnityCipher can be implemented AES encryption(Exactly, Rijndael cipher, not AES cryptography) and RSA encryption simply and high secure, in Unity(C#).
If you want to download a unitypackage, you can download from [Releases](https://github.com/TakuKobayashi/UnityCipher/releases).

# Usage
For detail, look to ```UnityCipher/Examples/```
And also, add ```using UnityCipher```, you can use UnityCipher.

## Use AES encryption
### Encryption
You can encrypt it by calling the following method.

```C#
string encrypted = RijndaelEncryption.Encrypt(planeText, passwordText);
```

```planeText``` can also use ```byte[]``` as well as string.
If you use ```byte[]```, give the encritpted ```byte[]```, like this.

```C#
byte[] encrypted = RijndaelEncryption.Encrypt(planeBinary, passwordText);
```

### Decription
You can decrypt it by calling the following method.

```C#
string planeText = RijndaelEncryption.Decrypt(encryptedText, passwordText);
```

If you can successfully decrypt the encrypted one, you can get the decrypted one.
```planeText``` can also use ```byte[]``` as well as string.
If you use ```byte[]```, give the decrypted ```byte[]```, like this.

```C#
byte[] planeBinary = RijndaelEncryption.Decrypt(encryptedBinary, passwordText);
```
