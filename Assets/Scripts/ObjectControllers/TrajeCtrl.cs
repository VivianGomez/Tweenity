using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajeCtrl : MonoBehaviour
{        
    bool touched = false;
    public Material matHand;
    public Material matYellow;

    public SkinnedMeshRenderer mrHandR;
    public SkinnedMeshRenderer mrHandL;

    public void OnTriggerEnter(Collider other)
    {
        if (!touched && other.tag =="IndexFinger")
        {
            touched = true;
            mrHandR.material = matYellow;
            mrHandL.material = matYellow;

            GameObject.Find("SimulationController").GetComponent<SimulationController>().VerifyUserAction(new SimulationObject.Action(gameObject.name, "Usar",""));
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
