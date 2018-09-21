using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour {

    [SerializeField] private TabButton RijndealButton;
    [SerializeField] private TabButton RSAButton;
    [SerializeField] private GameObject RijndealContent;
    [SerializeField] private GameObject RSAContent;

    // Use this for initialization
    void Start () {
        RijndealButton.OnSelect = () =>
        {
            RijndealButton.interactable = false;
            RSAButton.interactable = true;
            RSAContent.gameObject.SetActive(false);
            RijndealContent.gameObject.SetActive(true);
        };
        RSAButton.OnSelect = () =>
        {
            RSAButton.interactable = false;
            RijndealButton.interactable = true;
            RijndealContent.gameObject.SetActive(false);
            RSAContent.gameObject.SetActive(true);
        };
        RijndealButton.interactable = false;
        RSAButton.interactable = true;
        RSAContent.gameObject.SetActive(false);
        RijndealContent.gameObject.SetActive(true);
    }
}
