using UnityEngine;
using UnityEngine.UI;
using UnityCipher;

public class RijndaelContent : MonoBehaviour
{
    [SerializeField] private InputField textInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private InputField resultText;

    public void ExecuteEncrypt(){
        string planeText = textInput.text;
        string passwordText = passwordInput.text;
        string encrypted = RijndaelEncryption.Encrypt(planeText, passwordText);
        Debug.Log(encrypted);
        resultText.text = encrypted;
    }

    public void ExecuteDecrypt()
    {
        string encryptedText = textInput.text;
        string passwordText = passwordInput.text;
        string plane = RijndaelEncryption.Decrypt(encryptedText, passwordText);
        Debug.Log(plane);
        resultText.text = plane;
    }

    public void PasteToInputText(){
        textInput.text = resultText.text;
    }
}
