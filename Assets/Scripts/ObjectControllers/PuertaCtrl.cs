using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCtrl : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public int PlayAnimation(string name)
    {
        if(name=="open")
        {
            anim.SetBool("abrirPuerta", true);
        }
        else if(name=="close")
        {
            anim.SetBool("cerrarPuerta", true);
        }

        return 1000;
    }
}
