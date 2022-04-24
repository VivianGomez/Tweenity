
using UnityEngine;
using System;
using TMPro;

public class PntAlarmasController : MonoBehaviour
{
    public TextMeshProUGUI txtNombreAlarma;
    public TextMeshProUGUI txtTiempoAlarma;
    public TextMeshProUGUI txtHoraPnt;

    public TextMeshProUGUI txtRespuesta;
    public Renderer rendererAMS;


    public GameObject pantallaAlarmas;
    public GameObject pantallaHome;
    public GameObject avisoAlarma;

    private void Start()
    {
        if(avisoAlarma!=null)
        {
            avisoAlarma.SetActive(false);
            pantallaAlarmas.SetActive(false);
            pantallaHome.SetActive(true);
        }
        
    }

    void Update() {
        txtHoraPnt.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public void ActivarAlarmaABC(object alarma)
    {
        txtNombreAlarma.text = alarma.ToString();
        txtTiempoAlarma.text = DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss") + " - 01m - Aux";
        GameObject.Find("VentanasCuartoZ").GetComponent<Animator>().SetBool("activarAlarma", true);
        avisoAlarma.SetActive(true);
        avisoAlarma.GetComponent<Animator>().SetBool("activarAlarma", true);
        GameObject.Find("SonidoAlarmaCM").GetComponent<AudioSource>().Play();
        pantallaAlarmas.SetActive(true);
        pantallaHome.SetActive(false);
        GameObject.Find("ItemAlarma").GetComponent<Animator>().SetBool("activarAlarma", true);
        MostrarAlarmas();
    }

    public void MostrarAlarmas()
    {
        pantallaAlarmas.SetActive(true);
        pantallaHome.SetActive(false);
    }

    public void AceptarAlarma()
    {
        GameObject.Find("SonidoAlarmaCM").GetComponent<AudioSource>().Stop();
        GameObject.Find("VentanasCuartoZ").GetComponent<Animator>().SetBool("activarAlarma", false);
        avisoAlarma.SetActive(false);
        GameObject.Find("ItemAlarma").GetComponent<Animator>().SetBool("activarAlarma", false);
    }

    public void MostrarMensajeAceptar()
    {
        GameObject.Find("ItemAlarma").SetActive(false);
        GameObject.Find("BtnAceptar").SetActive(false);
        GameObject.Find("BtnIgnorar").SetActive(false);
        txtRespuesta.text = "Se ha enviado una alerta al personal de emergencias. Pero aún hay riesgo de incendio, debe llamar al oficial encargado.";

    }

    public void MostrarMensajeIgnorar()
    {
        GameObject.Find("ItemAlarma").SetActive(false);
        GameObject.Find("BtnAceptar").SetActive(false);
        GameObject.Find("BtnIgnorar").SetActive(false);
        txtRespuesta.text = "Ignorar esta alarma puede tener consecuencias fatales";
    }
    
}
