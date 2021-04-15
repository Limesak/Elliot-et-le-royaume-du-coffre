using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlidingButton : MonoBehaviour
{
    public Button MyButton;
    private Vector3 ORIGIN;
    private Vector3 ORIGINbanderole;
    public Transform SlidePos;
    private bool Slided;
    private UnityEngine.EventSystems.EventSystem ES;

    public GameObject Banderole;
    public Transform SlidePosBanderole;

    void Start()
    {
        ORIGIN = transform.localPosition;
        ORIGINbanderole = Banderole.transform.localPosition;
        Slided = false;
        ES = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
    }

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
                Banderole.transform.DOKill();
                Slided = true;
                transform.DOLocalMove(SlidePos.localPosition, 0.2f);
                Banderole.transform.DOLocalMove(SlidePosBanderole.localPosition, 0.2f);
            }
            
        }
        else
        {
            if (Slided)
            {
                transform.DOKill();
                Banderole.transform.DOKill();
                Slided = false;
                transform.DOLocalMove(ORIGIN, 0.2f);
                Banderole.transform.DOLocalMove(ORIGINbanderole, 0.2f);
            }
        }
    }
}
