using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CustomSliderButton : MonoBehaviour
{
    public float ValueOfButton;
    public Image ImageOfButton;

    private bool Selected;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (!Selected)
            {
                transform.DOKill();
                Selected = true;
                transform.DOScale(new Vector3(1,1.3f,1), 0.01f);
            }

        }
        else
        {
            if (Selected)
            {
                transform.DOKill();
                Selected = false;
                transform.DOScale(new Vector3(1, 1, 1), 0.01f);
            }
        }
    }
}
