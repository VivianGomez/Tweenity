using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSysController : MonoBehaviour
{
    public void ChangeParticleColor(string colorName)
    {
        switch (colorName)
        {
            case "red":
                GetComponent<ParticleSystem>().startColor = Color.red;
                break;
            case "green":
                GetComponent<ParticleSystem>().startColor = Color.green;
                break;
            case "blue":
                GetComponent<ParticleSystem>().startColor = Color.blue;
                break;
            case "yellow":
                GetComponent<ParticleSystem>().startColor = Color.yellow;
                break;
            case "pink":
                GetComponent<ParticleSystem>().startColor = new Vector4(1, 0.627451f, 0.9722671f, 1f);
                break;
            case "purple":
                GetComponent<ParticleSystem>().startColor = new Vector4(0.3773585f, 0.05482377f, 0.3535928f, 1f);
                break;
        }        
    }
}
