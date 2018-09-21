using System;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : Button {
    [SerializeField] private Text buttonText;

    public Action OnSelect = null;

    public string Text{
        set{
            buttonText.text = value;
        }
        get{
            return buttonText.text;
        }
    }

    public void OnButtonSelected(){
        if(OnSelect != null){
            this.OnSelect();
        }
    }
}
