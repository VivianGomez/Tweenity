using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LoggerGraph : MonoBehaviour
{
    public SimulationController simulationController;

    public TextMeshProUGUI currentNode;
    public TextMeshProUGUI nodeType;

    void Update()
    {
        currentNode.text = "<b>Nodo actual: </b>"+simulationController.GetCurrentNode().title;

        if(simulationController.GetCurrentNode().tags.Count > 0)
        {
            nodeType.text = "<b>Tipo de nodo: </b>"+simulationController.GetCurrentNode().tags[0];
        }
        else
        {
            nodeType.text = "<b>Tipo de nodo: </b> sin categoria";
        }
        
    }
}
