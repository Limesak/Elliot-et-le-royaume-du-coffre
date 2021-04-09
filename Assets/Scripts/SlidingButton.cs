using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlidingButton : MonoBehaviour
{
    public Button MyButton;
    private Vector3 ORIGIN;
    public Transform SlidePos;
    private bool Slided;
    private UnityEngine.EventSystems.EventSystem ES;

    void Start()
    {
        ORIGIN = transform.position;
        Slided = false;
        ES = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveParameter.current.canUseOnglets)
        {
            MyButton.interactable = true;
        }
        else
        {
            MyButton.interactable = false;
        }

        if(ES.currentSelectedGameObject == this.gameObject)
        {
            if (!Slided)
            {
                transform.DOKill();
                Slided = true;
                transform.DOMove(SlidePos.position, 0.2f);
            }
            
        }
        else
        {
            if (Slided)
            {
                transform.DOKill();
                Slided = false;
                transform.DOMove(ORIGIN, 0.2f);
            }
        }
    }
}
