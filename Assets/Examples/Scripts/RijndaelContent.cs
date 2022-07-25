using UnityEngine;
using UnityEngine.UI;
using UnityCipher;

public class RijndaelContent : MonoBehaviour
{
    [SerializeField] private InputField textInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private InputField resultText;

    public void ExecuteEncrypt(){
        string plainText = textInput.text;
        string passwordText = passwordInput.text;
        string encrypted = RijndaelEncryption.Encrypt(plainText, passwordText);
        Debug.Log(encrypted);
        resultText.text = encrypted;
    }

    public void ExecuteDecrypt()
    {
        string encryptedText = textInput.text;
        string passwordText = passwordInput.text;
        string plain = RijndaelEncryption.Decrypt(encryptedText, passwordText);
        Debug.Log(plain);
        resultText.text = plain;
    }

    public void PasteToInputText(){
        textInput.text = resultText.text;
    }
}
