using System;
using System.Collections;
using UnityEngine;

public class BtnTutorial : MonoBehaviour
{
    private Animator animator;

    // Control de activación 
    public bool activated = false;
    public bool pTouched = false;
    private Renderer rendererComp;

    public AudioClip soundDownEnabled;
    public AudioClip soundDownDisabled;

    void Start()
    {
        animator = GetComponent<Animator>();
        rendererComp = GetComponent<Renderer>();   
        pTouched = false;
    }

    public void ActiveAnimation()
    {
        activated = !activated;
        animator.SetBool("pressed", activated);

        if (activated)
        {
            AudioSource.PlayClipAtPoint(soundDownEnabled, Vector3.zero, 5.0f);
            rendererComp.material.SetFloat("_Metallic", 0f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(soundDownDisabled, Vector3.zero, 1.0f);
            rendererComp.material.SetFloat("_Metallic", 1f);
        }
    }

    public void ChangeBtnColor(string colorName)
    {
        switch (colorName)
        {
            case "red":
                rendererComp.material.SetColor("_Color", Color.red);
                break;
            case "green":
                rendererComp.material.SetColor("_Color", Color.green);
                break;
            case "blue":
                rendererComp.material.SetColor("_Color", Color.blue);
                break;
        }
    }

    public void Touched()
    {
        ActiveAnimation();
        GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "Touched",""));
    }


    /** Este método sirve para detectar que se oprime el botón en VR
    /*  Puede usarse con la mano virtual, en este caso se usa un collider en el dedo índice, 
    /*  para detectar la colisión entre el botón y el dedo
    **/
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (!pTouched && other.tag =="IndexFinger")
        {
            pTouched = true;
            Touched();
            yield return new WaitForSeconds(0.5f);    
            pTouched = false;    
        }
    }

    IEnumerator OnMouseDown()
    {
        if (!pTouched)
        {
            pTouched = true;
            Touched();
            yield return new WaitForSeconds(0.5f);    
            pTouched = false;    
        }
    }
}
