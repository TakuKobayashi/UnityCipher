using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[AddComponentMenu("UI/Advanced/Tab Controller")]
public class GUITabController : MonoBehaviour
{
    [SerializableAttribute]
    public struct TabContentsPair
    {
        public Button tab;
        public CanvasRenderer contentBody;

        public Optionals optionals;

        public void SetTabInteractable(bool b)
        {
            tab.interactable = b;
            if (optionals.selectedTab != null)
            {
                optionals.selectedTab.gameObject.SetActive(!b);
                tab.gameObject.SetActive(b);
            }
        }
    }

    [SerializableAttribute]
    public struct Optionals
    {
        public CanvasRenderer selectedTab;
    }

    public List<TabContentsPair> tabContentsPairs;

    // Use this for initialization
    void Start()
    {
        if (tabContentsPairs != null)
        {
            tabContentsPairs.ForEach(pair => {
                // initialize tab state
                pair.SetTabInteractable(!pair.contentBody.gameObject.activeSelf);
                // add click listener
                pair.tab.onClick.AddListener(() => {
                    // switch active contents
                    tabContentsPairs.ForEach(p => p.contentBody.gameObject.SetActive(false));
                    pair.contentBody.gameObject.SetActive(true);
                    // switch interactable
                    tabContentsPairs.ForEach(p => p.SetTabInteractable(true));
                    pair.SetTabInteractable(false);
                });
            });
        }
    }

}