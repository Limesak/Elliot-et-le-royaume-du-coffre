using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogueCharaInfo : MonoBehaviour
{
    public MenuManager.Character Name;

    public GameObject[] Emotions;

    public GameObject HIDDEN_POINT;
    public GameObject MovablePart;

    private Vector3 ORIGIN_POINT;

    private bool isTalking;
    private bool initTalking;

    private bool inWork;

    void Start()
    {
        ORIGIN_POINT = transform.localPosition;
        transform.localPosition = HIDDEN_POINT.transform.localPosition;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(initTalking && !inWork)
        {
            isTalking = true;
            RecursiveTalk();
        }
    }

    public void Talk()
    {
        if (!isTalking)
        {
            initTalking = true;
        }
    }

    public void StopTalking()
    {
        initTalking = false;
        isTalking = false;
    }

    private void RecursiveTalk()
    {
        if (isTalking && !inWork)
        {
            initTalking = false;
            MovablePart.transform.DOPunchPosition(Vector3.up*25f,0.5f,1,0.5f).OnComplete(() => { inWork = false; RecursiveTalk(); });
        }
    }

    public void PopEmotion(int index)
    {
        for(int i = 0; i < Emotions.Length; i++)
        {
            if(i == index)
            {
                Emotions[i].SetActive(true);
            }
            else
            {
                Emotions[i].SetActive(false);
            }
        }
    }

    public void Wake()
    {
        inWork = true;
        transform.DOLocalMove(ORIGIN_POINT, 0.4f + Random.Range(0, 0.5f)).OnComplete(() => { inWork = false; });
    }

    public void PutToSleep()
    {
        MovablePart.transform.DOKill();
        transform.DOKill();
        transform.DOLocalMove(HIDDEN_POINT.transform.localPosition, 0.1f+Random.Range(0,0.5f)).OnComplete(() => { gameObject.SetActive(false); });
    }
}
