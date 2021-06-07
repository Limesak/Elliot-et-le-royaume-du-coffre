using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class TutoManager : MonoBehaviour
{
    [Header("POS")]
    public GameObject PosOpen;
    public GameObject PosClose;

    [Header("BUTTON")]
    public GameObject Button;

    [Header("TUTOS")]
    public GameObject TUTO_DeplacemenNCamera;
    public GameObject TUTO_EquiperObjet;
    public GameObject TUTO_Epee;
    public GameObject TUTO_Course;

    private GameObject[] FourreToutList;
    private bool isOn;

    private ElliotSoundSystem ESS;


    void Start()
    {
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
        FourreToutList = new GameObject[] { TUTO_DeplacemenNCamera, TUTO_EquiperObjet, TUTO_Epee , TUTO_Course };
        Button.SetActive(false);
        isOn = false;
        for (int i = 0; i < FourreToutList.Length; i++)
        {

            if (FourreToutList[i].activeSelf)
            {
                FourreToutList[i].transform.localPosition = PosClose.transform.localPosition;
                FourreToutList[i].SetActive(false);
            }
        }
    }

    public void CloseAll()
    {
        for(int i = 0; i < FourreToutList.Length; i++)
        {
            if (FourreToutList[i].activeSelf)
            {
                if (FourreToutList[i] == TUTO_DeplacemenNCamera)
                {
                    SaveData.current.TutoDone_Deplacement = true;
                }
                else if (FourreToutList[i] == TUTO_EquiperObjet)
                {
                    SaveData.current.TutoDone_Equiper = true;
                }
                else if (FourreToutList[i] == TUTO_Epee)
                {
                    SaveData.current.TutoDone_Sword = true;
                }
                else if (FourreToutList[i] == TUTO_Course)
                {
                    SaveData.current.TutoDone_Course = true;
                }

                ESS.PlaySound(ESS.OneOf(ESS.UI_CARNET_PageTournee), ESS.Asource_Interface, 0.4f, false);
                FourreToutList[i].transform.DOLocalMove(PosClose.transform.localPosition, 0.3f).OnComplete(() => { FourreToutList[i].SetActive(false); });
            }
        }
        Button.SetActive(false);
        SaveParameter.current.canUseInputs = true;
        isOn = false;
    }

    public void Open(GameObject fiche)
    {
        if (SaveData.current.CurrentDifficulty != 2 && SaveData.current.CurrentDifficulty != 4 )
        {
            fiche.SetActive(true);
            fiche.transform.localPosition = PosClose.transform.localPosition;
            fiche.transform.DOLocalMove(PosOpen.transform.localPosition, 0.3f);
            ESS.PlaySound(ESS.OneOf(ESS.UI_CARNET_PageTournee), ESS.Asource_Interface, 0.4f, false);
            Button.SetActive(true);
            EventSystem.current.SetSelectedGameObject(Button);
            SaveParameter.current.canUseInputs = false;
            isOn = true;
        }
        else
        {
            if (fiche == TUTO_DeplacemenNCamera)
            {
                SaveData.current.TutoDone_Deplacement = true;
            }
            else if (fiche == TUTO_EquiperObjet)
            {
                SaveData.current.TutoDone_Equiper = true;
            }
            else if (fiche == TUTO_Epee)
            {
                SaveData.current.TutoDone_Sword = true;
            }

            /*
                if (FourreToutList[i].name == "Tuto Deplacement et Camera")
                {
                    SaveData.current.TutoDone_Deplacement = true;
                }
                else if (FourreToutList[i].name == "Tuto Equiper Objet")
                {
                    SaveData.current.TutoDone_Equiper = true;
                }
                else if (FourreToutList[i].name == "Tuto Epee")
                {
                    SaveData.current.TutoDone_Sword = true;
                }
                */
        }

        
    }

    public bool isInTuto()
    {
        return isOn;
    }
}
