using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("FirstButtonOfMenuPage")]

    public GameObject Button_ContinuerPartie;
    public GameObject Button_NouvellePartie;
    public GameObject Button_NonNePasEcraser;

    [Header("StandAlone Menus")]

    public GameObject Menu_ContinueOuNouvelle;
    public GameObject Menu_EcraserOuAnnuler;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void PopMenuCoffre()
    {
        Menu_ContinueOuNouvelle.SetActive(true);
        if (SaveLoad.alreadySavedGame())
        {
            EventSystem.current.SetSelectedGameObject(Button_ContinuerPartie);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(Button_NouvellePartie);
        }
    }

    public void PopMenuCoffreConfirmation()
    {
        Menu_EcraserOuAnnuler.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Button_NonNePasEcraser);
    }

    public void PlaySave()
    {

    }

    public void TryPlayNewSave()
    {
        if (SaveLoad.alreadySavedGame())
        {
            PopMenuCoffreConfirmation();
        }
        else
        {
            PlayNewSave();
        }
    }

    public void PlayNewSave()
    {
        SaveData.current.ResetValueToDefault();
        SaveLoad.Save(SaveData.current);
        SceneManager.LoadScene("TestTheo", LoadSceneMode.Single);
    }

    public void QuitAllMenu()
    {
        Menu_ContinueOuNouvelle.SetActive(false);
        Menu_EcraserOuAnnuler.SetActive(false);
    }
}
