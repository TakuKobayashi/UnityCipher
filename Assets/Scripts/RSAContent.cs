using UnityEngine;
using UnityEngine.UI;
using UnityCipher;
using System.Collections.Generic;

public class RSAContent : MonoBehaviour
{
    [SerializeField] private InputField textInput;
    [SerializeField] private InputField sizeInput;
    [SerializeField] private Text publicKeyText;
    [SerializeField] private Text privateKeyText;
    [SerializeField] private Text resultText;

    private KeyValuePair<string, string> publicAndPrivateKeyValuePair;

    public void GenarateKeyPair()
    {
        publicAndPrivateKeyValuePair = RSAEncryption.GenrateKeyPair(int.Parse(sizeInput.text));
        publicKeyText.text = publicAndPrivateKeyValuePair.Key;
        privateKeyText.text = publicAndPrivateKeyValuePair.Value;
    }

    public void ExecuteEncrypt(){
        if(string.IsNullOrEmpty(publicKeyText.text)){
            return;
        }
        string planeText = textInput.text;
        string encrypted = RSAEncryption.Encrypt(planeText, publicAndPrivateKeyValuePair.Key);
        resultText.text = encrypted;
    }

    public void ExecuteDecrypt()
    {
        if (string.IsNullOrEmpty(privateKeyText.text))
        {
            return;
        }
        string encryptedText = textInput.text;
        string plane = RSAEncryption.Decrypt(encryptedText, publicAndPrivateKeyValuePair.Value);
        resultText.text = plane;
    }
}
