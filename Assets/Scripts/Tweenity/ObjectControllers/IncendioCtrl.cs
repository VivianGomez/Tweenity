using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncendioCtrl : MonoBehaviour
{
    ParticleSystem fire1;
    ParticleSystem fire2;

    void Start()
    {
        fire1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        fire2 = transform.GetChild(1).GetComponent<ParticleSystem>();
        fire2.gameObject.GetComponent<AudioSource>().Stop();
        fire1.Stop();
        fire2.Stop();
    }

    public void ActivarFuego()
    {
        fire1.Play();
        fire2.Play();
        fire2.gameObject.GetComponent<AudioSource>().Play();
        GameObject.Find("SonidoAlarmaCM").GetComponent<AudioSource>().Play();
    }
}
