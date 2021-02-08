using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [Header("Accessor")]

    public PlayerMovement PM;

    [Header("FirstButtonOfMenuPage")]

    public GameObject Button_ContinuerPartie;
    public GameObject Button_NouvellePartie;
    public GameObject Button_NonNePasEcraser;

    [Header("StandAlone Menus")]

    public GameObject Menu_ContinueOuNouvelle;
    public GameObject Menu_EcraserOuAnnuler;


    void Start()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        QuitAllMenu();
    }


    void Update()
    {
        
    }

    public void ClassicShitsPop()
    {
        SaveParameter.current.canUseInputs = false;
    }

    public void PopMenuCoffre()
    {
        ClassicShitsPop();
        Menu_ContinueOuNouvelle.SetActive(true);
        if (SaveLoad.alreadySavedGame())
        {
            EventSystem.current.SetSelectedGameObject(Button_ContinuerPartie);
            Button_ContinuerPartie.GetComponent<Button>().interactable = true;
            Debug.Log("Save found");
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(Button_NouvellePartie);
            Button_ContinuerPartie.GetComponent<Button>().interactable = false;
            Debug.Log("No save");
        }
    }

    public void PopMenuCoffreConfirmation()
    {
        ClassicShitsPop();
        Menu_EcraserOuAnnuler.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Button_NonNePasEcraser);
    }

    public void PlaySave()
    {
        Debug.Log("PlaySave");
        QuitAllMenu();
        SaveData.current = (SaveData) SaveLoad.Load();
        SceneManager.LoadScene("DJN_Academie1", LoadSceneMode.Single);
    }

    public void TryPlayNewSave()
    {
        if (SaveLoad.alreadySavedGame())
        {
            Debug.Log("Save found");
            QuitAllMenu();
            PopMenuCoffreConfirmation();
        }
        else
        {
            Debug.Log("No save");
            PlayNewSave();
        }
    }

    public void PlayNewSave()
    {
        Debug.Log("PlayNewSave");
        QuitAllMenu();
        SaveData.current.ResetValueToDefault();
        SaveLoad.Save(SaveData.current);
        SceneManager.LoadScene("DJN_Academie1", LoadSceneMode.Single);
    }

    public void QuitAllMenu()
    {
        Menu_ContinueOuNouvelle.SetActive(false);
        Menu_EcraserOuAnnuler.SetActive(false);
        SaveParameter.current.canUseInputs = true;
    }
}
