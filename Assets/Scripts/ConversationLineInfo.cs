using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLineInfo : MonoBehaviour
{
    [Header("Inspector Info")]
    [Tooltip("Only in editor, don't appear in game, takomprimacouille")]
    public string LineName;
    [Header("Character on left")]
    public MenuManager.Character PersoLeft;
    public int PersoLeftEmotionIndex;
    [Header("Character on right")]
    public MenuManager.Character PersoRight;
    public int PersoRightEmotionIndex;
    [Header("Line infos")]
    public MenuManager.WIT WhoIsTalking;
    public string LineContent;
    [Tooltip("Put true if there is a Pass ButtonAction before, takomprimacouille")]
    public bool isNotSkippable;
    [Header("Buttons infos")]
    public MenuManager.DialogueActionButton ActionButtonA;
    public string ButtonAContent;
    public MenuManager.DialogueActionButton ActionButtonB;
    public bool isButtonBPresent;
    public string ButtonBContent;
    [Header("Branch infos")]
    [Tooltip("Put the index of the desired branch from the ConversationInfo Branch Table if one of the buttonAction is Branch, takomprimacouille")]
    public int branchIndex;
    [Header("ChangeMission")]
    public bool doChangeTheMission;
    public string NewMission;
    [Header("AddHint")]
    public bool doAddHint;
    public string NewHint;
}
