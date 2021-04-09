using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    Movements MovementsControls;

    [Header("Accessor")]

    public PlayerMovement PM;
    public bool isUnfading;

    [Header("BlackScreen")]

    public Image BlackScreen;
    public float Speed;

    [Header("FirstButtonOfMenuPage")]

    public GameObject Button_ContinuerPartie;
    public GameObject Button_NouvellePartie;
    public GameObject Button_NonNePasEcraser;

    [Header("StandAlone Menus")]

    public GameObject Menu_ContinueOuNouvelle;
    public GameObject Menu_EcraserOuAnnuler;

    [Header("Main Menus")]

    public GameObject Carnet_GLOBAL;
    public GameObject Carnet_OPEN;
    public GameObject[] Carnet_Pages;
    private Vector3 Carnet_ORIGIN;
    private Vector3 Carnet_ORIGIN_scale;
    public Transform Carnet_HiddenPos;
    private bool MenuOn;
    public GameObject ButtonFirstMainMenu;


    void Start()
    {
        Carnet_ORIGIN = Carnet_GLOBAL.transform.position;
        Carnet_ORIGIN_scale = Carnet_GLOBAL.transform.localScale;
        MenuOn = false;
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 1);
        isUnfading = false;
        Carnet_GLOBAL.transform.position = Carnet_HiddenPos.position;
        Carnet_GLOBAL.transform.localScale = Carnet_HiddenPos.localScale;
        Carnet_OPEN.SetActive(false);
        Carnet_GLOBAL.SetActive(false);
        StartCoroutine(Unfade());
        QuitAllMenu();
    }

    void Awake()
    {
        MovementsControls = new Movements();
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Menu.started += ctx => OpenOrKillMainMenu();
            MovementsControls.Player.Interact.started += ctx => Cancel();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Menu.started += ctx => OpenOrKillMainMenu();
            MovementsControls.Player1.Interact.started += ctx => Cancel();
        }
    }

    public void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Menu.Enable();
            MovementsControls.Player.Interact.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Menu.Enable();
            MovementsControls.Player1.Interact.Enable();
        }

    }

    public void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Menu.Disable();
            MovementsControls.Player.Interact.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Menu.Disable();
            MovementsControls.Player1.Interact.Disable();
        }

    }


    void Update()
    {
        //Debug.Log("Last button: " + EventSystem.current.currentSelectedGameObject);
        Debug.Log("Can use inputs: " + SaveParameter.current.canUseInputs);

        if(MenuOn || Menu_ContinueOuNouvelle.activeSelf || Menu_EcraserOuAnnuler.activeSelf)
        {
            SaveParameter.current.canUseInputs = false;
        }
        else
        {
            SaveParameter.current.canUseInputs = true;
        }
    }

    public void PopMenuCoffre()
    {
        SaveParameter.current.canUseInputs = false;
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
        SaveParameter.current.canUseInputs = false;
    }

    public void PopMenuCoffreConfirmation()
    {
        SaveParameter.current.canUseInputs = false;
        Menu_EcraserOuAnnuler.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Button_NonNePasEcraser);
    }

    public void PlaySave()
    {
        Debug.Log("PlaySave");
        QuitAllMenu();
        SaveData.current = (SaveData) SaveLoad.Load();
        StartCoroutine(FadeNLoad());
    }

    public void TryPlayNewSave()
    {
        if (SaveLoad.alreadySavedGame())
        {
            Debug.Log("Save found");
            Menu_ContinueOuNouvelle.SetActive(false);
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
        SaveData.current.currentScene = 1;
        SaveLoad.Save(SaveData.current);
        StartCoroutine(FadeNLoad());
    }

    public void Cancel()
    {
        if (!SaveParameter.current.canUseInputs)
        {
            CloseMainMenu();
        }
    }

    public void OpenOrKillMainMenu()
    {
        if (!MenuOn && !Menu_ContinueOuNouvelle.activeSelf && !Menu_EcraserOuAnnuler.activeSelf)
        {
            SaveParameter.current.canUseInputs = false;
            MenuOn = true;
            Carnet_GLOBAL.SetActive(true);
            if (Carnet_OPEN.activeSelf)
            {
                Carnet_GLOBAL.transform.DOKill();
                Carnet_GLOBAL.transform.DOScale(Carnet_ORIGIN_scale, 0.19f);
                Carnet_GLOBAL.transform.DOMove(Carnet_ORIGIN, 0.2f).OnComplete(() => { EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu); });
            }
            else
            {
                Carnet_GLOBAL.transform.DOKill();
                Carnet_GLOBAL.transform.DOScale(Carnet_ORIGIN_scale, 0.39f);
                Carnet_GLOBAL.transform.DOMove(Carnet_ORIGIN, 0.4f).OnComplete(() => { Carnet_OPEN.SetActive(true); EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu); });
            }
        }
        else
        {
            SaveParameter.current.canUseInputs = true;
            MenuOn = false;
            Carnet_GLOBAL.transform.DOKill();
            Carnet_GLOBAL.transform.DOScale(Carnet_HiddenPos.localScale, 0.19f);
            Carnet_GLOBAL.transform.DOMove(Carnet_HiddenPos.position, 0.2f).OnComplete(() => { Carnet_GLOBAL.SetActive(false); });
        }
    }

    public void CloseMainMenu()
    {
        SaveParameter.current.canUseInputs = true;
        Carnet_OPEN.SetActive(false);
        MenuOn = false;
        Carnet_GLOBAL.transform.DOKill();
        Carnet_GLOBAL.transform.DOScale(Carnet_HiddenPos.localScale, 0.29f);
        Carnet_GLOBAL.transform.DOMove(Carnet_HiddenPos.position, 0.3f).OnComplete(() => { Carnet_GLOBAL.SetActive(false); });
    }

    public void QuitAllMenu()
    {
        Menu_ContinueOuNouvelle.SetActive(false);
        Menu_EcraserOuAnnuler.SetActive(false);
        SaveParameter.current.canUseInputs = true;
    }

    IEnumerator Fade()
    {
        while (BlackScreen.color.a < 1)
        {
            if (BlackScreen.color.a + Speed * Time.deltaTime >= 1)
            {
                BlackScreen.color = new Color(0, 0, 0, 1);
            }
            else
            {
                BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a + Speed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeNLoad()
    {
        
        while (BlackScreen.color.a < 1)
        {
            if (BlackScreen.color.a + Speed * Time.deltaTime >= 1)
            {
                BlackScreen.color = new Color(0, 0, 0, 1);
            }
            else
            {
                BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a + Speed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Loading");
    }

    IEnumerator Unfade()
    {
        while (BlackScreen.color.a > 0)
        {
            isUnfading = true;
            if (BlackScreen.color.a + Speed * Time.deltaTime <= 0)
            {
                BlackScreen.color = new Color(0, 0, 0, 0);
            }
            else
            {
                BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a - Speed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        isUnfading = false;
    }
}
