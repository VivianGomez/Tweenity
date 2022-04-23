using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMeshPath : MonoBehaviour
{
    public bool hidden;
    // Start is called before the first frame update
    void Start()
    {
        if (hidden)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
