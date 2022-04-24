using System;
using System.Collections.Generic;
using UnityEngine;

public class BtnsManager : MonoBehaviour
{
    public GameObject btn1;
    public GameObject btn2;

    public void ShowButton(object btnName, object show)
    {
        bool stateShow = ((""+show)=="true")
                    ? true
                    : false;

        switch (btnName)
        {
            case "button1":
                btn1.SetActive(stateShow);
                break;
            case "button2":
                btn2.SetActive(stateShow);
                break;
        }
    }
}
