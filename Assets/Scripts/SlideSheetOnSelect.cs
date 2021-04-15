﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlideSheetOnSelect : MonoBehaviour
{
    public Button MyButton;
    public GameObject MySheet;
    private Vector3 ORIGIN;
    public Transform SlidePos;
    private bool Slided;
    private UnityEngine.EventSystems.EventSystem ES;

    public GameObject Ring;
    private Vector3 ScaleOrigin;

    void Start()
    {
        ORIGIN = MySheet.transform.localPosition;
        ScaleOrigin = Ring.transform.localScale;
        Ring.transform.localScale = Vector3.zero;
        Slided = false;
        ES = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
    }

    void Update()
    {

        if (ES.currentSelectedGameObject == this.gameObject)
        {
            if (!Slided)
            {
                transform.DOKill();
                Slided = true;
                MySheet.transform.DOLocalMove(SlidePos.localPosition, 0.2f);
                Ring.transform.DOScale(ScaleOrigin, 0.1f);
            }

        }
        else
        {
            if (Slided)
            {
                transform.DOKill();
                Slided = false;
                MySheet.transform.DOLocalMove(ORIGIN, 0.2f);
                Ring.transform.DOScale(Vector3.zero, 0.1f);
            }
        }
    }
}
