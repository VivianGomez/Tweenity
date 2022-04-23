using UnityEngine;
using System;

/*
 * Mediante este Script se controlan los diferentes efectos de sonido posibles en el juego
 * */
public class GuardiaVoice : MonoBehaviour
{
    static AudioSource audioSource;

    //Se inicializan los sonidos usando variables AudioClip y los audios guardados en Resources/audios/
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // El nombre del mob deberá ser una variable global
    public static int PlayVoice(string clip)
    {
        print("playing... "+ clip);
        AudioClip audioClip = Resources.Load<AudioClip>("Audios/"+SimulationController.currentDirectoryAudios+"/"+clip);
        audioSource.clip = audioClip;
        audioSource.Play();
        print(""+audioSource.clip.length*1000);
        return int.Parse(""+ Convert.ToInt32(audioSource.clip.length*1000));   
    }
}
