using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinematicStep : MonoBehaviour
{
    public enum StepType { JustWait, MoveCamera,QuitCamera, LaunchCinematicScript,PopDialogue, Fade, Unfade,EndCinematic, AddLineToDiaryBuffer , ChangeMission, AddHint};

    [Header("Global infos")]
    public StepType Type;
    public bool waitOtherScriptSignal;
    public float waitDuration;
    [Header("MoveCamera")]
    public CinemachineFreeLook CinematicCamera;
    [Header("LaunchCinematicScript")]
    public CinematicScript[] Scripts;
    [Header("PopDialogue")]
    public ConversationInfo Conversation;
    [Header("AddLineToDiaryBuffer")]
    public string BufferLine;
    [Header("ChangeMission")]
    public string MissionLine;
    [Header("AddHint")]
    public string HintLine;

}
