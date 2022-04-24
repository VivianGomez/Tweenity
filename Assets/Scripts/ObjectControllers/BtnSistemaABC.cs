using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSistemaABC : MonoBehaviour
{
    bool pTouched = false;
    public Material matPressed;
    public Material matActive;

    public void Pressed()
    {
        GameObject.Find("SistemaABC").GetComponent<PntAlarmasController>().AceptarAlarma();
        GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "Pressed",""));
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (!pTouched && other.tag =="IndexFinger")
        {
            pTouched = true;
            GetComponent<Renderer>().material = matPressed;
            Pressed();
            yield return new WaitForSeconds(0.5f);    
            pTouched = false;    
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag =="IndexFinger")
        {
            GetComponent<Renderer>().material = matActive;
            pTouched = false;    
        }
    }
}
