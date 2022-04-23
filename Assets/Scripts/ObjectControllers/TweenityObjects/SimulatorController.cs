//
//  SimulatorController.cs
//  Tweenity
//
//  Created by Vivian Gómez.
//  Copyright © 2021 Vivian Gómez - Pablo Figueroa - Universidad de los Andes
//

using System;
using System.Threading.Tasks;
using UnityEngine;

public class SimulatorController : MonoBehaviour
{
    public DialogueViewer dialogueViewer;

    public void ShowReminder(object countdown, object activeObject, object audioName)
    {
        GameObject.Find("Reminder").GetComponent<ReminderController>().MoveOverObject(activeObject.ToString().Trim());
        if(audioName.ToString() != ""){VoiceController.PlayVoice(audioName.ToString());}
    }

    public void Timeout(object countdown)
    {
        //Acá se puede incluir una implementación personalizada del timeout
    }

    public int Wait(object time)
    {
        return Convert.ToInt32(time)*1000;
    }

    public void InitializeAudiosDirectory(string dirAudio)
    {
        SimulationController.currentDirectoryAudios = dirAudio;
    }

    public int Play(object audioName)
    {
        return VoiceController.PlayVoice(audioName.ToString());
    }

    public int PlayLipsyncAvatar(object audioName)
    {
        return GuardiaVoice.PlayVoice(audioName.ToString());
    }

    public void RelocateCharacter(string nombrePersonaje, object x, object y, object z)
    {
        print(x);
        GameObject.Find(nombrePersonaje).transform.position= new Vector3(float.Parse(""+x), float.Parse(""+y), float.Parse(""+z));
    }

    public async void MoveCharacter(string nombrePersonaje, string nombrePath)
    {
        MoveCharWithAnimation moveChar = GameObject.Find(nombrePersonaje).transform.GetChild(0).gameObject.GetComponent<MoveCharWithAnimation>();
        moveChar.setPathBase(GameObject.Find(nombrePath));
        await moveChar.StartMove();
    }

    public void OpenDialogueViewer()
    {
        dialogueViewer.OpenDialogue();
    }

    public void CloseDialogueViewer()
    {
        dialogueViewer.CloseDialogue();
    }
}
