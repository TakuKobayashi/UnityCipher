public struct RijndaelEncripter
{
    public readonly byte[] Salt;
    public readonly byte[] IV;
    public readonly byte[] Encrypted;

    internal RijndaelEncripter(byte[] salt, byte[] iv, byte[] encrypted){
        Salt = salt;
        IV = iv;
        Encrypted = encrypted;
    }
}
