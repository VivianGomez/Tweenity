using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public void Activate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
