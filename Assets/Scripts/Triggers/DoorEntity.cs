using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorEntity : MonoBehaviour
{
    public GameObject Door;
    public GameObject OPEN_Pos;
    public GameObject CLOSED_Pos;
    public float AnimDuration;
    public float RandomnessAmplitude;

    public void OpenTheDoor()
    {
        float rdm = Random.Range(-RandomnessAmplitude, RandomnessAmplitude);
        Door.transform.DOKill();
        Door.transform.DOLocalMove(OPEN_Pos.transform.localPosition, AnimDuration + rdm);
    }

    public void CloseTheDoor()
    {
        float rdm = Random.Range(-RandomnessAmplitude, RandomnessAmplitude);
        Door.transform.DOKill();
        Door.transform.DOLocalMove(CLOSED_Pos.transform.localPosition, AnimDuration + rdm);
    }

    public void TryButFailOpen()
    {
        float rdm = Random.Range(-RandomnessAmplitude, RandomnessAmplitude);
        Door.transform.DOKill();
        Door.transform.DOLocalJump(CLOSED_Pos.transform.localPosition, -0.2f,1,0.4f);
    }
}
