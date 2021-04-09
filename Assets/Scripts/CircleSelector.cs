using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CircleSelector : MonoBehaviour
{
    public Button MyButton;
    private Vector3 ORIGINscale;
    public GameObject Circle;
    private bool Selected;
    private UnityEngine.EventSystems.EventSystem ES;

    void Start()
    {
        ORIGINscale = Circle.transform.localScale;
        Circle.transform.localScale = Vector3.zero;
        ES = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ES.currentSelectedGameObject == MyButton.gameObject)
        {
            if (!Selected)
            {
                transform.DOKill();
                Selected = true;
                Circle.transform.DOScale(ORIGINscale, 0.2f);
            }

        }
        else
        {
            if (Selected)
            {
                transform.DOKill();
                Selected = false;
                Circle.transform.DOScale(Vector3.zero, 0.2f);
            }
        }
    }
}
