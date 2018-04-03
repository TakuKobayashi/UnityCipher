using UnityEngine;
using UnityEngine.UI;
using UnityCipher;

public class RijndaelContent : MonoBehaviour
{
    [SerializeField] private InputField textInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private Text resultText;

    public void ExecuteEncrypt(){
        string planeText = textInput.text;
        string passwordText = passwordInput.text;
        string encrypted = RijndaelEncryption.Encrypt(planeText, passwordText);
        resultText.text = encrypted;
    }

    public void ExecuteDecrypt()
    {
        string encryptedText = textInput.text;
        string passwordText = passwordInput.text;
        string plane = RijndaelEncryption.Decrypt(encryptedText, passwordText);
        resultText.text = plane;
    }
}
