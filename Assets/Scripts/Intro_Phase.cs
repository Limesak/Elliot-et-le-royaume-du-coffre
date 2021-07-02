using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Phase : MonoBehaviour
{
    public enum Type { None, MoveObject, PlaySound, Fade, Unfade, Pop, Depop, ChangeCamTarget,GoToMenu };

    public Type ActualType;
    public float Date;
    public bool Done;
    public GameObject Ref1;
    public GameObject Ref2;
    public AudioClip AudioClipRef;
    public AudioSource AudioSourceRef;
    public float FloatRef;
    
}
