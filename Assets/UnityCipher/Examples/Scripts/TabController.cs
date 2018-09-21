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
            ChangeTheTab(false);
        };
        RSAButton.OnSelect = () =>
        {
            ChangeTheTab(true);
        };
        ChangeTheTab(false);
    }

    private void ChangeTheTab(bool isToRSA){
        RijndealButton.interactable = isToRSA;
        RSAButton.interactable = !isToRSA;
        RSAContent.gameObject.SetActive(isToRSA);
        RijndealContent.gameObject.SetActive(!isToRSA);
    }
}
