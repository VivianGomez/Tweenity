using System;
using UnityEngine;

public class VoiceController : MonoBehaviour
{
    static AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public static int PlayVoice(string clip)
    {        
        int delay = 1000;
        AudioClip audioClip = Resources.Load<AudioClip>("Audios/"+SimulationController.currentDirectoryAudios+"/"+clip);
        audioSource.clip = audioClip;
        
        if(audioClip != null)
        {
            print("playing... "+ audioClip.name);
            audioSource.Play();
            delay = int.Parse(""+(Convert.ToInt32(audioSource.clip.length*1000)));
        }
        else
        {
            Debug.LogError("El audio  "+ clip + " no fue encontrado en la ruta Audios/"+SimulationController.currentDirectoryAudios+"/"+clip);
        }

        return delay;
    }
}
