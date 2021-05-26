using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PolaroidButton : MonoBehaviour
{
    public int index;
    public MenuManager.TypeOfPolaroid Type;
    public MenuManager MM;
    public bool isFiller;
    public Button BUTTON;
    public GameObject UnknownIllu;
    public Transform POS_Before;
    public Transform POS_Mid;
    public Transform POS_After;
    private UnityEngine.EventSystems.EventSystem ES;
    public SlideSheetOnSelect SSOS;

    void Start()
    {
        ES = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        if (!isFiller)
        {
            CheckUnknown();
        }

        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ES.currentSelectedGameObject == BUTTON.gameObject)
        {
            if (Type == MenuManager.TypeOfPolaroid.Bestiaire)
            {
                if (MM.CODEX_Bestiaire_CurrentIndex != index)
                {
                    MM.CODEX_Bestiaire_CurrentIndex = index;
                    MM.CODEX_Setup();
                }
            }
            else if (Type == MenuManager.TypeOfPolaroid.Lieux)
            {
                if (MM.CODEX_Lieux_CurrentIndex != index)
                {
                    MM.CODEX_Lieux_CurrentIndex = index;
                    MM.CODEX_Setup();
                }
            }
            else if (Type == MenuManager.TypeOfPolaroid.Souvenirs)
            {
                if (MM.CODEX_Souvenirs_CurrentIndex != index)
                {
                    MM.CODEX_Souvenirs_CurrentIndex = index;
                    MM.CODEX_Setup();
                }
            }

        }

    }

    public void CheckUnknown()
    {
        if (Type == MenuManager.TypeOfPolaroid.Bestiaire)
        {
            if (SaveData.current.Codex_Bestiaire_unlockList[index])
            {
                UnknownIllu.SetActive(false);
            }
            else
            {
                UnknownIllu.SetActive(true);
            }
        }
        else if (Type == MenuManager.TypeOfPolaroid.Lieux)
        {
            if (SaveData.current.Codex_Lieux_unlockList[index])
            {
                UnknownIllu.SetActive(false);
            }
            else
            {
                UnknownIllu.SetActive(true);
            }
        }
        else if (Type == MenuManager.TypeOfPolaroid.Souvenirs)
        {
            if (SaveData.current.Codex_Souvenirs_unlockList[index])
            {
                UnknownIllu.SetActive(false);
            }
            else
            {
                UnknownIllu.SetActive(true);
            }
        }

        if (SSOS != null)
        {
            if (Type == MenuManager.TypeOfPolaroid.Bestiaire)
            {
                if (SaveData.current.Codex_Bestiaire_unlockList[index])
                {
                    SSOS.SetDontSlideSheet(false);
                }
                else
                {
                    SSOS.SetDontSlideSheet(true);
                }
            }
            else if (Type == MenuManager.TypeOfPolaroid.Lieux)
            {
                if (SaveData.current.Codex_Lieux_unlockList[index])
                {
                    SSOS.SetDontSlideSheet(false);
                }
                else
                {
                    SSOS.SetDontSlideSheet(true);
                }
            }
            else if (Type == MenuManager.TypeOfPolaroid.Souvenirs)
            {
                if (SaveData.current.Codex_Souvenirs_unlockList[index])
                {
                    SSOS.SetDontSlideSheet(false);
                }
                else
                {
                    SSOS.SetDontSlideSheet(true);
                }
            }
        }
    }

    public void GoMid()
    {
        transform.DOKill();
        transform.DOLocalMove(POS_Mid.localPosition,0.1f);
        //CheckUnknown();
    }

    public void GoLeft()
    {
        transform.DOKill();
        transform.DOLocalMove(POS_Before.localPosition, 0.1f);
        //CheckUnknown();
    }

    public void GoRight()
    {
        transform.DOKill();
        transform.DOLocalMove(POS_After.localPosition, 0.1f);
        //CheckUnknown();
    }

    public void PopMid()
    {
        transform.localPosition = POS_Mid.localPosition;
        //CheckUnknown();
    }

    public void PopLeft()
    {
        transform.localPosition = POS_Before.localPosition;
        //CheckUnknown();
    }

    public void PopRight()
    {
        transform.localPosition = POS_After.localPosition;
        //CheckUnknown();
    }

    private void OnEnable()
    {
        CheckUnknown();
    }
}
