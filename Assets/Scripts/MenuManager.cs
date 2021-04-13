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
    public HandManager HM;
    public DiaryManager DM;

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
    public GameObject ReturnButton;

    [Header("Main Menus MISSION")]

    public Text Memoire;
    public Text Infos;
    public Text Day;
    private int currentDay;
    public Button GoBef;
    public Button GoAft;

    [Header("Main Menus CARTE")]

    public GameObject MainMap;
    public GameObject Map1;

    [Header("Main Menus EQUIPEMENT")]

    public GameObject DecriptionSheet;
    public GameObject SpotHead;
    public GameObject SpotBack;
    public GameObject SpotShield;
    public GameObject SpotSword;
    public GameObject[] SpotsOFF;
    public GameObject[] Buttons;
    public GameObject[] Illus;


    void Start()
    {
        ReturnButton.SetActive(false);
        GoBef.interactable = false;
        GoAft.interactable = false;
        Carnet_ORIGIN = Carnet_GLOBAL.transform.position;
        Carnet_ORIGIN_scale = Carnet_GLOBAL.transform.localScale;
        MenuOn = false;
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        HM = GameObject.FindGameObjectWithTag("Player").GetComponent<HandManager>();
        DM = GameObject.FindGameObjectWithTag("Player").GetComponent<DiaryManager>();
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

    //--------------------  MAIN MENU SHiTS  -------------------------------------------------------------------------------------------

    public void Cancel()
    {
        if (!SaveParameter.current.canUseInputs)
        {
            if(SaveParameter.current.MMTMP.CS == MenuMemoryTMP.CancelState.Main)
            {
                CloseMainMenu();
            }
            else if (SaveParameter.current.MMTMP.CS == MenuMemoryTMP.CancelState.InOpenMap)
            {
                SaveParameter.current.canUseOnglets = true;
                ReturnButton.SetActive(false);
                EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu);
                MainMap.SetActive(true);
                Map1.SetActive(false);
                SaveParameter.current.MMTMP.CS = MenuMemoryTMP.CancelState.Main;
            }
            
        }
    }

    public void OpenPage(int index)
    {
        SaveParameter.current.MMTMP.index = index;
        for (int i = 0; i < Carnet_Pages.Length; i++)
        {
            if(i == index)
            {
                Carnet_Pages[i].SetActive(true);
            }
            else
            {
                Carnet_Pages[i].SetActive(false);
            }
        }

        if (index == 0)
        {
            MISSION_PopCurrentDay();
            MISSION_checkborders();
            EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu);
        }
        else if (index == 2)
        {
            STUFF_PopButton();
        }
    }

    public void MISSION_PopCurrentDay()
    {
        currentDay = SaveData.current.Diary.Length;
        MISSION_PopDay();
    }

    public void MISSION_goBefore()
    {
        currentDay = currentDay-1;
        MISSION_PopDay();
        MISSION_checkborders();
    }

    public void MISSION_goAfter()
    {
        currentDay = currentDay+1;
        MISSION_PopDay();
        MISSION_checkborders();
    }

    public void MISSION_checkborders()
    {
        if (MenuOn && SaveParameter.current.MMTMP.index == 0)
        {
            if (currentDay == 0)
            {
                if (GoBef.IsInteractable())
                {
                    GoBef.interactable = false;
                    EventSystem.current.SetSelectedGameObject(GoAft.gameObject);
                }

            }
            else
            {
                GoBef.interactable = true;
            }

            if (currentDay == SaveData.current.Diary.Length)
            {
                if (GoAft.IsInteractable())
                {
                    GoAft.interactable = false;
                    EventSystem.current.SetSelectedGameObject(GoBef.gameObject);
                }
            }
            else
            {
                GoAft.interactable = true;
            }
        }
    }

    private void MISSION_PopDay()
    {
        int realDay = currentDay + 1;
        Day.text = "Jour " + realDay;
        Memoire.text = DM.GetAdventureOf(currentDay);
        Infos.text = DM.GetMissionOf(currentDay);
    }

    public void CARTE_Show(int index)
    {
        if(index == 0)
        {
            ReturnButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(ReturnButton);
            MainMap.SetActive(false);
            Map1.SetActive(true);
            SaveParameter.current.canUseOnglets = false;
            SaveParameter.current.MMTMP.CS = MenuMemoryTMP.CancelState.InOpenMap;
            
        }
        else if(index == 1)
        {

        }
        else
        {

        }
    }

    public void STUFF_PopButton()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            if (SaveData.current.UnlockList[i])
            {
                Buttons[i].SetActive(true);
                if (i == 0)//HEAD
                {
                    if(i == SaveData.current.CurrentItemHEAD)
                    {
                        Buttons[i].transform.localPosition = SpotHead.transform.localPosition;
                        Illus[i].SetActive(true);
                    }
                    else
                    {
                        Buttons[i].transform.localPosition = SpotsOFF[i].transform.localPosition;
                        Illus[i].SetActive(false);
                    }
                }
                else if (i == 1)//BACK
                {
                    if (i == SaveData.current.CurrentItemBACK)
                    {
                        Buttons[i].transform.localPosition = SpotBack.transform.localPosition;
                        Illus[i].SetActive(true);
                    }
                    else
                    {
                        Buttons[i].transform.localPosition = SpotsOFF[i].transform.localPosition;
                        Illus[i].SetActive(false);
                    }
                }
                else if (i == 2)//SWORD
                {
                    if (i == SaveData.current.CurrentItemSWORD)
                    {
                        Buttons[i].transform.localPosition = SpotSword.transform.localPosition;
                        Illus[i].SetActive(true);
                    }
                    else
                    {
                        Buttons[i].transform.localPosition = SpotsOFF[i].transform.localPosition;
                        Illus[i].SetActive(false);
                    }
                }
                else if (i == 3)//SHIELD
                {
                    if (i == SaveData.current.CurrentItemSHIELD)
                    {
                        Buttons[i].transform.localPosition = SpotShield.transform.localPosition;
                        Illus[i].SetActive(true);
                    }
                    else
                    {
                        Buttons[i].transform.localPosition = SpotsOFF[i].transform.localPosition;
                        Illus[i].SetActive(false);
                    }
                }
            }
            else
            {
                Buttons[i].SetActive(false);
                Illus[i].SetActive(false);
            }
        }
    }

    public void STUFF_EquipeOrUnequipThis(int index)
    {
        if (index == 0)//HEAD
        {
            if(index == SaveData.current.CurrentItemHEAD)
            {
                SaveData.current.CurrentItemHEAD = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f);
                Illus[index].SetActive(false);
            }
            else
            {
                if(SaveData.current.CurrentItemHEAD == -1)
                {
                    SaveData.current.CurrentItemHEAD = index;
                    Buttons[index].transform.DOLocalMove(SpotHead.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemHEAD].SetActive(false);
                    Buttons[SaveData.current.CurrentItemHEAD].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemHEAD].transform.localPosition, 0.3f);
                    SaveData.current.CurrentItemHEAD = index;
                    Buttons[index].transform.DOLocalMove(SpotHead.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
            }
        }
        else if (index == 1)//BACK
        {
            if (index == SaveData.current.CurrentItemBACK)
            {
                SaveData.current.CurrentItemBACK = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f);
                Illus[index].SetActive(false);
            }
            else
            {
                if (SaveData.current.CurrentItemBACK == -1)
                {
                    SaveData.current.CurrentItemBACK = index;
                    Buttons[index].transform.DOLocalMove(SpotBack.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemBACK].SetActive(false);
                    Buttons[SaveData.current.CurrentItemBACK].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemBACK].transform.localPosition, 0.3f);
                    SaveData.current.CurrentItemBACK = index;
                    Buttons[index].transform.DOLocalMove(SpotBack.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
            }
        }
        else if (index == 2)//SWORDS
        {
            if (index == SaveData.current.CurrentItemSWORD)
            {
                SaveData.current.CurrentItemSWORD = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f);
                Illus[index].SetActive(false);
            }
            else
            {
                if (SaveData.current.CurrentItemSWORD == -1)
                {
                    SaveData.current.CurrentItemSWORD = index;
                    Buttons[index].transform.DOLocalMove(SpotSword.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemSWORD].SetActive(false);
                    Buttons[SaveData.current.CurrentItemSWORD].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemSWORD].transform.localPosition, 0.3f);
                    SaveData.current.CurrentItemSWORD = index;
                    Buttons[index].transform.DOLocalMove(SpotSword.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
            }
        }
        else if (index == 3)//SHIELDS
        {
            if (index == SaveData.current.CurrentItemSHIELD)
            {
                SaveData.current.CurrentItemSHIELD = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f);
                Illus[index].SetActive(false);
            }
            else
            {
                if (SaveData.current.CurrentItemSHIELD == -1)
                {
                    SaveData.current.CurrentItemSHIELD = index;
                    Buttons[index].transform.DOLocalMove(SpotShield.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemSHIELD].SetActive(false);
                    Buttons[SaveData.current.CurrentItemSHIELD].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemSHIELD].transform.localPosition, 0.3f);
                    SaveData.current.CurrentItemSHIELD = index;
                    Buttons[index].transform.DOLocalMove(SpotShield.transform.localPosition, 0.3f);
                    Illus[index].SetActive(true);
                }
            }
        }
        HM.UpdateClothes();
        HM.UpdateHands();
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
                OpenPage(SaveParameter.current.MMTMP.index);
                if (SaveParameter.current.MMTMP.index==2)
                {
                    STUFF_PopButton();
                }
                EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu);
                Carnet_GLOBAL.transform.DOKill();
                Carnet_GLOBAL.transform.DOScale(Carnet_ORIGIN_scale, 0.19f);
                Carnet_GLOBAL.transform.DOMove(Carnet_ORIGIN, 0.2f);
            }
            else
            {
                Carnet_GLOBAL.transform.DOKill();
                Carnet_GLOBAL.transform.DOScale(Carnet_ORIGIN_scale, 0.39f);
                Carnet_GLOBAL.transform.DOMove(Carnet_ORIGIN, 0.4f).OnComplete(() => { Carnet_OPEN.SetActive(true); OpenPage(0);  EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu); });
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

    // ---------------------   BLACK SCREEN   --------------------------------------------------------

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
