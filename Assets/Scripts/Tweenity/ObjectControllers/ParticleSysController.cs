using UnityEngine;
using System.Collections;

public class ParticleSysController : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.MainModule main;

    void Start() {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
    }

    public void ChangeParticleColor(string colorName)
    {
        switch (colorName)
        {
            case "red":
                main.startColor = Color.red;
                break;
            case "green":
                main.startColor = Color.green;
                break;
            case "blue":
                main.startColor = Color.blue;
                break;
            case "yellow":
                main.startColor = Color.yellow;
                break;
            case "pink":
                main.startColor = new Color(1, 0.627451f, 0.9722671f, 1f);
                break;
            case "purple":
                main.startColor = new Color(0.3773585f, 0.05482377f, 0.3535928f, 1f);
                break;
        }        
    }
}
