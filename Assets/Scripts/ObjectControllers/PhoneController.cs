using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{
    public TextMeshProUGUI txtPhoneScreen; 
    public GameObject btnLlamar;
    AudioSource audioSource;

    public bool descolgado = false;
    bool enLlamada = false;

    Vector3 initialPosition;
    Quaternion initialRotation;

    OVRGrabbable grabbable;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();
        btnLlamar.SetActive(false);
        grabbable = GetComponent<OVRGrabbable>();
    }

    public void MostrarTexto(string texto="Llamar al puente")
    {
        txtPhoneScreen.text = texto;
    }

    public void Descolgar()
    {
        print("calling... ");
        AudioClip audioClip = Resources.Load<AudioClip>("Audio/phone-call-tone");
        audioSource.clip = audioClip;
        audioSource.Play();
        txtPhoneScreen.text = "Oprima el botón azul para llamar al puente";
        btnLlamar.SetActive(true);
    }

    public void Llamar()
    {
        audioSource.loop = false;
        audioSource.Stop();
        txtPhoneScreen.text = "...";
    }

    public void AudioRespuestaTelefono(object nombreAudio)
    {
        txtPhoneScreen.text = "En llamada con el puente";
        print("escuchando... ");
        AudioClip audioClip = Resources.Load<AudioClip>("Audio/MOB1/"+nombreAudio);
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void OnTriggerStay(Collider other)
    {
        if(descolgado && other.gameObject.name == "CajTelefono")
        {
            ReiniciarEstado();
        }

        else if(!enLlamada && other.tag == "MainCamera")
        {
            Descolgar();
            enLlamada=true;
        }
        
        else if(!descolgado && GetComponent<OVRGrabbable>().isGrabbed && !(other.tag == "MainCamera") && !(other.tag == "CajTelefono"))
        {
            txtPhoneScreen.text = "Acerque el teléfono a su cabeza para escuchar";
            descolgado = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(descolgado)
        {   
            if(other.gameObject.name == "CajTelefono" || other.gameObject.name=="TabPanelPropulsion" || other.gameObject.name == "Plano Piso")
            {
                ColgarCorrectamente();
            }
        }
    }

    public void ColgarCorrectamente()
    {
        ReiniciarEstado();
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
    

    public void CambiarEstadoDescolgado(object pDescolgado)
    {
        if(pDescolgado.ToString()=="true")
        {
            descolgado = true;
        }
        else
        {
            ReiniciarEstado();
        }
    }

    public void ReiniciarEstado()
    {
        descolgado = false;
        enLlamada=false;
        MostrarTexto("Descuelgue para llamar");
        if(btnLlamar!=null)
        {
            btnLlamar.GetComponent<BtnLlamarPuente>().ReiniciarBtn();
            btnLlamar.SetActive(false);
        }
    }

}
