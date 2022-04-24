using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLlamarPuente : MonoBehaviour
{
    public PhoneController phoneController;
    Animator animator;

    public bool llamo = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!llamo && other.tag =="IndexFinger")
        {
            phoneController.Llamar();
            animator.SetBool("hide", true);
            llamo = true;
            GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "Touched",""));
        }
    }

    public void ReiniciarBtn()
    {
        animator = GetComponent<Animator>();
        llamo = false;
        animator.SetBool("hide", false);
    }
}
