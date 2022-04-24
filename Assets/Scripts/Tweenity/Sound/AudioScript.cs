using UnityEngine;

//Mediante este Script se cambian los sonidos de ambiente en el juego
public class AudioScript : MonoBehaviour
{

    // Objeto tipo AudioClip, es el sonido a reproducir
    public AudioClip MusicClip;

    // El componente Audiosource se usa para reproducir el sonido 3D
    private AudioSource MusicSource;

    // Al comenzar el juego, cuando se activa el script se da play al sonido
    void Start()
    {
        MusicSource = gameObject.GetComponent<AudioSource>();
        MusicSource.clip = MusicClip;
        MusicSource.Play();
    }
}
