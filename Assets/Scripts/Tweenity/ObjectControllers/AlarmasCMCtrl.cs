using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmasCMCtrl : MonoBehaviour
{
     public void ActivarAlarmaCM()
    {
        GameObject.Find("SonidoAlarmaCM").GetComponent<AudioSource>().Play();
        GameObject.Find("CMV1").GetComponent<Animator>().SetBool("activarAlarma", true);
        GameObject.Find("CMV2").GetComponent<Animator>().SetBool("activarAlarma", true);
    }

    public void DesactivarAlarmaCM()
    {
        GameObject.Find("SonidoAlarmaCM").GetComponent<AudioSource>().Stop();
        GameObject.Find("CMV1").GetComponent<Animator>().SetBool("activarAlarma", false);
        GameObject.Find("CMV2").GetComponent<Animator>().SetBool("activarAlarma", false);
    }
}
