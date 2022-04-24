using UnityEngine;
using System;
using TMPro;

public class ItemAMSCtrl : MonoBehaviour
{
    public int step = 1;
    public string age = "23h";
    void Start()
    {
        SetDateTime();
    }

    public void SetDateTime()
    {   
        DateTime now = DateTime.Now;
        gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (new DateTime(now.Year, now.Month, now.Day-step).ToString("dd/MM/yyyy"))+now.ToString("-HH:mm:ss")+" - "+age+" - Aux";
    }
}
