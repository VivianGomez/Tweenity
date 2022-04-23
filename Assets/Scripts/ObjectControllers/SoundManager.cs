using UnityEngine;
using System;

/*
 * Mediante este Script se controlan los diferentes efectos de sonido posibles en el juego
 * */
public class SoundManager : MonoBehaviour
{
    public static AudioClip phoneRing, ansCall, alarmaCM, zafarranchoIncendio, wrongAnswer;
    static AudioSource audioSource;

    //Se inicializan los sonidos usando variables AudioClip y los audios guardados en Resources/audios/
    void Awake()
    {
        phoneRing = Resources.Load<AudioClip>("Audio/phone-ring");
        //ansCall = Resources.Load<AudioClip>("Audio/beep_call");
        zafarranchoIncendio = Resources.Load<AudioClip>("Audio/zafarranchoIncendio");
        wrongAnswer = Resources.Load<AudioClip>("Audio/wrongAnswer");

        audioSource = GetComponent<AudioSource>();
    }

    //Se reproduce el sonido dependiendo del nombre que llega por parámetro
    public static void PlaySound(string clip)
    {
        print("playing... "+ clip);
        audioSource.loop = false;
        audioSource.Stop();
        audioSource.volume = 1f;
        
        switch (clip)
        {
            case "phoneRing":
                audioSource.loop = true;
                audioSource.clip = (phoneRing);
                audioSource.Play();
                break;
            case "alarmaCM":
                audioSource.PlayOneShot(alarmaCM);
                break;
            case "wrongAnswer":
                audioSource.PlayOneShot(wrongAnswer);
                break;
            case "zafarranchoIncendio":
                audioSource.volume = 0.08f;
                audioSource.PlayOneShot(zafarranchoIncendio);
                break;
        }
    }

    // El nombre del mob deberá ser una variable global
    public static int PlayVoice(string clip)
    {
        print("playing... "+ clip);
        AudioClip audioClip = Resources.Load<AudioClip>("Audio/"+SimulationController.currentDirectoryAudios+"/"+clip);
        audioSource.clip = audioClip;
        audioSource.Play();
        print(""+audioSource.clip.length*1000);
        return int.Parse(""+ Math.Truncate(audioSource.clip.length)*1000);   
    }

    public void SubirVolumen(object nuevoVol)
    {
        GetComponent<AudioSource>().volume = float.Parse(""+nuevoVol);
    }
}
