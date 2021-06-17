using System.Collections;
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
    public bool dontShowRing;

    public bool GrowOnSelect;

    private ElliotSoundSystem ESS;

    public bool DontSlideSheet;


    void Start()
    {
        ORIGIN = MySheet.transform.localPosition;
        if (!dontShowRing)
        {
            ScaleOrigin = Ring.transform.localScale;
            Ring.transform.localScale = Vector3.zero;
        }
        Slided = false;
        ES = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
    }

    void Update()
    {

        if (ES.currentSelectedGameObject == this.gameObject && !DontSlideSheet)
        {
            if (!Slided )
            {
                transform.DOKill();
                Slided = true;
                MySheet.transform.DOLocalMove(SlidePos.localPosition, 0.02f);
                ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.3f, false);
                if (!dontShowRing)
                {
                    Ring.transform.DOScale(ScaleOrigin, 0.01f);
                    ESS.PlaySound(ESS.OneOf(ESS.UI_CARNET_CrayonEcrit), ESS.Asource_Interface, 0.1f, false);
                }
                if (GrowOnSelect)
                {
                    transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.02f);
                }
            }

        }
        else
        {
            if (Slided)
            {
                transform.DOKill();
                Slided = false;
                MySheet.transform.DOLocalMove(ORIGIN, 0.02f);
                ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminEnroule), ESS.Asource_Interface, 0.1f, false);
                if (!dontShowRing)
                {
                    Ring.transform.DOScale(Vector3.zero, 0.01f);
                    Ring.transform.DOScale(Vector3.zero, 0.01f);
                }
                if (GrowOnSelect)
                {
                    transform.DOScale(new Vector3(1, 1, 1), 0.2f);
                }
            }
        }

        if (GrowOnSelect && DontSlideSheet)
        {
            if (ES.currentSelectedGameObject == this.gameObject )
            {
                if (GrowOnSelect)
                {
                    transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.02f);
                }
            }
            else
            {
                if (GrowOnSelect)
                {
                    transform.DOScale(new Vector3(1, 1, 1), 0.02f);
                }
            }
        }
        
    }

    public void GoBackToNormalSize()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetDontSlideSheet(bool b)
    {
        DontSlideSheet = b;
        if (!b)
        {
            MySheet.transform.localPosition = ORIGIN;
        }
        Slided = false;
    }
}
