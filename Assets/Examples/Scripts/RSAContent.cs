using UnityEngine;
using UnityEngine.UI;
using UnityCipher;
using System.Collections.Generic;

public class RSAContent : MonoBehaviour
{
    [SerializeField] private InputField textInput;
    [SerializeField] private InputField sizeInput;
    [SerializeField] private InputField publicKeyText;
    [SerializeField] private InputField privateKeyText;
    [SerializeField] private InputField resultText;
    [SerializeField] private GameObject cryptoField;

    private KeyValuePair<string, string> publicAndPrivateKeyValuePair;

    private void Start()
    {
        cryptoField.gameObject.SetActive(false);
    }

    public void GenarateKeyPair()
    {
        cryptoField.gameObject.SetActive(true);
        publicAndPrivateKeyValuePair = RSAEncryption.GenrateKeyPair(int.Parse(sizeInput.text));
        Debug.Log(publicAndPrivateKeyValuePair.Key);
        Debug.Log(publicAndPrivateKeyValuePair.Value);
        publicKeyText.text = publicAndPrivateKeyValuePair.Key;
        privateKeyText.text = publicAndPrivateKeyValuePair.Value;
    }

    public void ExecuteEncrypt(){
        if(string.IsNullOrEmpty(publicKeyText.text)){
            return;
        }
        string planeText = textInput.text;
        string encrypted = RSAEncryption.Encrypt(planeText, publicAndPrivateKeyValuePair.Key);
        Debug.Log(encrypted);
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
        Debug.Log(plane);
        resultText.text = plane;
    }

    public void PasteToInputText()
    {
        textInput.text = resultText.text;
    }
}
