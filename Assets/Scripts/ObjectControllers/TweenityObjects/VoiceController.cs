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
        print("playing... "+ audioClip.name);
        
        audioSource.Play();
        if(audioClip != null)
        {
            delay = int.Parse(""+(Convert.ToInt32(audioSource.clip.length*1000)));
        }

        return delay;
    }
}
