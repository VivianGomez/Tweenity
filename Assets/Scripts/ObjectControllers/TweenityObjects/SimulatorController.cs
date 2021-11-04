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

    public void TimeOut(object countdown)
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

    public void OpenDialogueViewer()
    {
        dialogueViewer.OpenDialogue();
    }

    public void CloseDialogueViewer()
    {
        dialogueViewer.CloseDialogue();
    }
}
