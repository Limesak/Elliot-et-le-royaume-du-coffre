using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinematicManager : MonoBehaviour
{
    private bool isInCinematic;
    private CinematicStep[] CurrentCinematic;
    private int CurrentIndex;
    private CinemachineFreeLook LastCinematicCamera;
    public DiaryManager DM;
    public NotifManager NM;

    public MenuManager MM;

    void Start()
    {
        isInCinematic = false;
        CurrentCinematic = null;
        CurrentIndex = 0;
        LastCinematicCamera = null;
        NM = GameObject.FindGameObjectWithTag("CanvasUI").GetComponent<NotifManager>();
    }

    void Update()
    {
        /*
        if(isInCinematic && CurrentCinematic == null)
        {
            isInCinematic = false;
        }
        */
    }

    public void StartNewCinematic(CinematicStep[] CC)
    {
        if (!isInCinematic)
        {
            isInCinematic = true;
            CurrentCinematic = CC;
            CurrentIndex = 0;
            LaunchCurrentStep();
        }
    }

    public void LaunchCurrentStep()
    {
        //Debug.Log("Launching step "+ CurrentIndex + "/" + (CurrentCinematic.Length-1) + "  :  " + CurrentCinematic[CurrentIndex].Type.ToString());
        if (CurrentIndex >= CurrentCinematic.Length)
        {
            //Debug.Log("Aborting because Steps.Length = " + CurrentCinematic.Length);
            isInCinematic = false;
            CurrentCinematic = null;
            CurrentIndex = 0;
            LastCinematicCamera = null;
            return;
        }

        switch (CurrentCinematic[CurrentIndex].Type)
        {
            case CinematicStep.StepType.JustWait:
                //Nothing to do;
                break;
            case CinematicStep.StepType.MoveCamera:
                if (LastCinematicCamera != null)
                {
                    LastCinematicCamera.m_Priority = 0;
                }
                LastCinematicCamera = CurrentCinematic[CurrentIndex].CinematicCamera;
                CurrentCinematic[CurrentIndex].CinematicCamera.m_Priority = 50;
                //Debug.Log("Trying to set up cam " + CurrentCinematic[CurrentIndex].CinematicCamera.gameObject.name);
                break;
            case CinematicStep.StepType.QuitCamera:
                if (LastCinematicCamera != null)
                {
                    LastCinematicCamera.m_Priority = 0;
                }
                break;
            case CinematicStep.StepType.LaunchCinematicScript:
                for(int i = 0; i< CurrentCinematic[CurrentIndex].Scripts.Length; i++)
                {
                    CurrentCinematic[CurrentIndex].Scripts[i].ExecuteScript();
                }
                break;
            case CinematicStep.StepType.PopDialogue:
                //Debug.Log("Trying to set up dialogue " + CurrentCinematic[CurrentIndex].Conversation.gameObject.name);
                MM.DIALOGUE_OpenDialogue(CurrentCinematic[CurrentIndex].Conversation);
                break;
            case CinematicStep.StepType.Fade:
                StartCoroutine(MM.Fade());
                break;
            case CinematicStep.StepType.Unfade:
                StartCoroutine(MM.Unfade());
                break;
            case CinematicStep.StepType.EndCinematic:
                StartCoroutine(MM.Unfade());
                if (LastCinematicCamera != null)
                {
                    LastCinematicCamera.m_Priority = 0;
                }
                isInCinematic = false;
                CurrentCinematic = null;
                CurrentIndex = 0;
                LastCinematicCamera = null;
                break;
            case CinematicStep.StepType.AddLineToDiaryBuffer:
                DM.AddBufferEntry(CurrentCinematic[CurrentIndex].BufferLine);
                break;
            case CinematicStep.StepType.ChangeMission:
                DM.ChangeTheMission(CurrentCinematic[CurrentIndex].MissionLine);
                break;
            case CinematicStep.StepType.AddHint:
                DM.AddHint(CurrentCinematic[CurrentIndex].HintLine);
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }

        if (isInCinematic && !CurrentCinematic[CurrentIndex].waitOtherScriptSignal)
        {
            StartCoroutine(GoLaunchStepAferWaitingX(CurrentCinematic[CurrentIndex].waitDuration));
        }

        CurrentIndex++;
    }

    IEnumerator GoLaunchStepAferWaitingX(float X)
    {
        //Debug.Log("Launching next step in " + X +" secondes");
        yield return new WaitForSeconds(X);
        LaunchCurrentStep();
    }

    public bool inCinematic()
    {
        return isInCinematic;
    }
}
