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
    private ElliotSoundSystem ESS;

    void Start()
    {
        ORIGINscale = Circle.transform.localScale;
        Circle.transform.localScale = Vector3.zero;
        ES = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
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
                Circle.transform.DOScale(ORIGINscale, 0.02f);
                ESS.PlaySound(ESS.OneOf(ESS.UI_CARNET_CrayonEcrit), ESS.Asource_Interface, 0.8f, false);
            }

        }
        else
        {
            if (Selected)
            {
                transform.DOKill();
                Selected = false;
                Circle.transform.DOScale(Vector3.zero, 0.02f);
            }
        }
    }
}
