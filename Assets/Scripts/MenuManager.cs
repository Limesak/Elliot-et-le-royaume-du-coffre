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
    public enum Character { None, Elliot, LecheCuillere, Poussierin };
    public enum TypeOfPolaroid { Bestiaire, Lieux, Souvenirs };
    public enum WIT { None, Left, Both, Right };
    public enum DialogueActionButton { None, Next, Pass, Quit , Branch, CinematicNextStep};
    Movements MovementsControls;

    [Header("Accessor")]

    public PlayerMovement PM;
    public bool isUnfading;
    public HandManager HM;
    public DiaryManager DM;
    private ElliotSoundSystem ESS;
    public CinematicManager CM;
    public BonbonUseManager BM;
    public TutoManager TM;

    [Header("BlackScreen")]

    public Image BlackScreen;
    public float Speed;

    [Header("FirstButtonOfMenuPage")]

    public GameObject Button_ContinuerPartie;
    public GameObject Button_NouvellePartie;
    public GameObject Button_NonNePasEcraser;
    public GameObject Button_Rester; 
    public GameObject Button_NePasQuitter;

    [Header("StandAlone Menus")]

    public GameObject Menu_ContinueOuNouvelle;
    public GameObject Menu_EcraserOuAnnuler;
    public GameObject Menu_QuitterOuRester;
    public GameObject Menu_ChambreOuRester;

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
    public GameObject SpotAmu;
    public GameObject[] SpotsOFF;
    public GameObject[] Buttons;
    public GameObject[] Illus;

    [Header("Main Menus UNIQUES")]
    public GameObject CEINTURE_Maillet;
    public GameObject POCHE_Money;
    public GameObject POCHE_Candy;
    public GameObject POCHE_YellowKey;

    [Header("Main Menus CODEX")]
    public int CODEX_Bestiaire_CurrentIndex;
    public int CODEX_Lieux_CurrentIndex;
    public int CODEX_Souvenirs_CurrentIndex;
    public PolaroidButton[] CODEX_Bestiaire_List;
    public PolaroidButton[] CODEX_Lieux_List;
    public PolaroidButton[] CODEX_Souvenirs_List;

    [Header("Main Menus SETTINGS")]
    public int SETTINGS_CurrentIndex;
    public GameObject[] SETTINGS_SecMenuClosed;
    public GameObject[] SETTINGS_SecMenuOpen;
    public GameObject[] SETTINGS_SecMenuIcone;
    public GameObject[] SETTINGS_SecMenuButton;
    public GameObject SETTINGS_OpenDiffCardPOS;
    public GameObject SETTINGS_CloseDiffCardPOS;
    public GameObject[] SETTINGS_DiffCards;
    public GameObject[] SETTINGS_DiffNames;
    public GameObject[] SETTINGS_DiffiButtons;

    [Header("DIALOGUES")]
    public GameObject DIALOGUE_ButtonA;
    public GameObject DIALOGUE_ButtonB;
    private ConversationInfo CurrentConv;
    private bool isDialogueOn;
    public Text DIALOGUE_ButtonAtext;
    public Text DIALOGUE_ButtonBtext;
    private int ConvCurrentIndex;
    private int BranchCurrentIndex;
    private string DialogueCurrentText;
    private bool isWritingDialogue;
    public float writingCharacterDuration;
    private float lastCharacterWroteDate;
    public Text DIALOGUE_TextContent;
    public float BlipBloupCooldown;
    private float lastBlipBloup;
    public Image BlackScreenForDialogue;
    public float MaxAlphaValue;
    private bool isUnfadingForDialogue;

    [Header("DIALOGUES Scrolls")]
    public GameObject CLOSED_SCROLL_HIDDEN_POS;
    private Vector3 CLOSED_SCROLL_ORIGIN;
    public GameObject ClosedScroll;
    public GameObject RollingScroll;
    public GameObject FlatScroll;
    public GameObject FLAT_SCROLL_HIDDEN_POS;
    private Vector3 FLAT_SCROLL_ORIGIN;

    [Header("DIALOGUES Characters")]
    public DialogueCharaInfo[] CharactersOnLeft;
    public DialogueCharaInfo[] CharactersOnRight;


    void Start()
    {
        CLOSED_SCROLL_ORIGIN = ClosedScroll.transform.localPosition;
        ClosedScroll.transform.localPosition = CLOSED_SCROLL_HIDDEN_POS.transform.localPosition;
        ClosedScroll.SetActive(false);
        RollingScroll.SetActive(false);
        FLAT_SCROLL_ORIGIN = FlatScroll.transform.localPosition;
        FlatScroll.SetActive(false);
        isDialogueOn = false;
        ConvCurrentIndex = 0;
        BranchCurrentIndex = 0;
        DialogueCurrentText = "";
        isWritingDialogue = false;
        SETTINGS_CurrentIndex = -1;

        ReturnButton.SetActive(false);
        GoBef.interactable = false;
        GoAft.interactable = false;
        Carnet_ORIGIN = Carnet_GLOBAL.transform.localPosition;
        Carnet_ORIGIN_scale = Carnet_GLOBAL.transform.localScale;
        MenuOn = false;
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        HM = GameObject.FindGameObjectWithTag("Player").GetComponent<HandManager>();
        DM = GameObject.FindGameObjectWithTag("Player").GetComponent<DiaryManager>();
        CM = GameObject.FindGameObjectWithTag("Player").GetComponent<CinematicManager>();
        BM = GameObject.FindGameObjectWithTag("Player").GetComponent<BonbonUseManager>();
        TM = GameObject.FindGameObjectWithTag("CanvasUI").GetComponent<TutoManager>();
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 1);
        BlackScreenForDialogue.gameObject.SetActive(true);
        BlackScreenForDialogue.color = new Color(0, 0, 0, 0);
        isUnfading = false;
        isUnfadingForDialogue = false;
        Carnet_GLOBAL.transform.localPosition = Carnet_HiddenPos.localPosition;
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
            MovementsControls.Player.Block.started += ctx => Cancel();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Menu.started += ctx => OpenOrKillMainMenu();
            MovementsControls.Player1.Block.started += ctx => Cancel();
        }
    }

    public void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Menu.Enable();
            MovementsControls.Player.Block.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Menu.Enable();
            MovementsControls.Player1.Block.Enable();
        }

    }

    public void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Menu.Disable();
            MovementsControls.Player.Block.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Menu.Disable();
            MovementsControls.Player1.Block.Disable();
        }

    }


    void Update()
    {
        //Debug.Log("Last button: " + EventSystem.current.currentSelectedGameObject);
        //Debug.Log("Can use inputs: " + SaveParameter.current.canUseInputs);

        if(MenuOn || Menu_ContinueOuNouvelle.activeSelf || Menu_EcraserOuAnnuler.activeSelf || Menu_QuitterOuRester.activeSelf || Menu_ChambreOuRester.activeSelf || isDialogueOn || CM.inCinematic() || TM.isInTuto())
        {
            SaveParameter.current.canUseInputs = false;
            //Debug.Log("Stuck");
            if (MenuOn && (Menu_ContinueOuNouvelle.activeSelf || Menu_EcraserOuAnnuler.activeSelf || Menu_QuitterOuRester.activeSelf || Menu_ChambreOuRester.activeSelf || isDialogueOn || CM.inCinematic() || TM.isInTuto()))
            {
                CloseMainMenu();
            }
        }
        else
        {
            SaveParameter.current.canUseInputs = true;
        }

        if(isDialogueOn && isWritingDialogue && lastCharacterWroteDate + writingCharacterDuration <= Time.time)
        {
            if(DialogueCurrentText.Length == CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].LineContent.Length)
            {
                isWritingDialogue = false;
            }
            else
            {
                lastCharacterWroteDate = Time.time;
                DialogueCurrentText = DialogueCurrentText + CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].LineContent.ToCharArray()[DialogueCurrentText.Length];

                if (lastBlipBloup + BlipBloupCooldown < Time.time)
                {
                    lastBlipBloup = Time.time;
                    ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_BlipBloup), ESS.Asource_Interface, 0.8f, false);
                }
            }
            DIALOGUE_TextContent.text = DialogueCurrentText;
            
        }
        else
        {
            //Debug.Log("Not Writing");
        }

        
    }

    public void PopMenuCoffre()
    {
        ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.8f, false);
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
        ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.8f, false);
    }

    public void PlaySave()
    {
        Debug.Log("PlaySave");
        QuitAllMenu();
        SaveData.current = (SaveData) SaveLoad.Load();
        StartCoroutine(FadeNLoad());
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
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
            ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
            Debug.Log("No save");
            PlayNewSave();
        }
    }

    public void PlayNewSave()
    {
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
        Debug.Log("PlayNewSave");
        QuitAllMenu();
        SaveData.current.ResetValueToDefault();
        SaveData.current.currentScene = 5;
        SaveLoad.Save(SaveData.current);
        StartCoroutine(FadeNLoad());
    }

    //--------------------  MAIN MENU SHiTS  -------------------------------------------------------------------------------------------

    public void Cancel()
    {
        if (!SaveParameter.current.canUseInputs)
        {
            if (isDialogueOn)
            {
                if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].isButtonBPresent)
                {
                    DIALOGUE_ButtonBPressed();
                }
            }
            else if(MenuOn)
            {
                ESS.PlaySound(ESS.UI_Annuler, ESS.Asource_Interface, 0.8f, false);
                if (SaveParameter.current.MMTMP.CS == MenuMemoryTMP.CancelState.Main)
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
                else if (SaveParameter.current.MMTMP.CS == MenuMemoryTMP.CancelState.OnSettingsSecondaryMenu)
                {
                    SETTINGS_OpenSecondaryMenu(-1);
                    EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu);
                    SaveParameter.current.MMTMP.CS = MenuMemoryTMP.CancelState.Main;
                }
            }
            
            
        }
    }

    public void OpenPage(int index)
    {
        if(index != SaveParameter.current.MMTMP.index)
        {
            ESS.PlaySound(ESS.OneOf(ESS.UI_CARNET_PageTournee), ESS.Asource_Interface, 0.8f, false);
        }
        
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
        else if(index == 3)
        {
            UNIQUES_PopItems();
        }
        else if (index == 4)
        {
            CODEX_Setup();
        }
        else if (index == 5)
        {
            SETTINGS_OpenSecondaryMenu(SETTINGS_CurrentIndex);
            SETTINGS_UpdateDifficultyUI();
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
        ESS.PlaySound(ESS.OneOf(ESS.UI_CARNET_PageTournee), ESS.Asource_Interface, 0.8f, false);
    }

    public void MISSION_goAfter()
    {
        currentDay = currentDay+1;
        MISSION_PopDay();
        MISSION_checkborders();
        ESS.PlaySound(ESS.OneOf(ESS.UI_CARNET_PageTournee), ESS.Asource_Interface, 0.8f, false);
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
            ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.8f, false);
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
                else if (i == 4 || i == 5 || i == 6)//AMULETTES
                {
                    if (i == SaveData.current.CurrentItemAMU)
                    {
                        Buttons[i].transform.localPosition = SpotAmu.transform.localPosition;
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
        ESS.PlaySound(ESS.UI_CARNET_ScotchArrache, ESS.Asource_Interface, 0.8f, false);
        if (index == 0)//HEAD
        {
            if(index == SaveData.current.CurrentItemHEAD)
            {
                SaveData.current.CurrentItemHEAD = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                Illus[index].SetActive(false);
            }
            else
            {
                if(SaveData.current.CurrentItemHEAD == -1)
                {
                    SaveData.current.CurrentItemHEAD = index;
                    Buttons[index].transform.DOLocalMove(SpotHead.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemHEAD].SetActive(false);
                    Buttons[SaveData.current.CurrentItemHEAD].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemHEAD].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    SaveData.current.CurrentItemHEAD = index;
                    Buttons[index].transform.DOLocalMove(SpotHead.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
            }
        }
        else if (index == 1)//BACK
        {
            if (index == SaveData.current.CurrentItemBACK)
            {
                SaveData.current.CurrentItemBACK = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                Illus[index].SetActive(false);
            }
            else
            {
                if (SaveData.current.CurrentItemBACK == -1)
                {
                    SaveData.current.CurrentItemBACK = index;
                    Buttons[index].transform.DOLocalMove(SpotBack.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemBACK].SetActive(false);
                    Buttons[SaveData.current.CurrentItemBACK].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemBACK].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    SaveData.current.CurrentItemBACK = index;
                    Buttons[index].transform.DOLocalMove(SpotBack.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
            }
        }
        else if (index == 2)//SWORDS
        {
            if (index == SaveData.current.CurrentItemSWORD)
            {
                SaveData.current.CurrentItemSWORD = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                Illus[index].SetActive(false);
            }
            else
            {
                if (SaveData.current.CurrentItemSWORD == -1)
                {
                    SaveData.current.CurrentItemSWORD = index;
                    Buttons[index].transform.DOLocalMove(SpotSword.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemSWORD].SetActive(false);
                    Buttons[SaveData.current.CurrentItemSWORD].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemSWORD].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    SaveData.current.CurrentItemSWORD = index;
                    Buttons[index].transform.DOLocalMove(SpotSword.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
            }
        }
        else if (index == 3)//SHIELDS
        {
            if (index == SaveData.current.CurrentItemSHIELD)
            {
                SaveData.current.CurrentItemSHIELD = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                Illus[index].SetActive(false);
            }
            else
            {
                if (SaveData.current.CurrentItemSHIELD == -1)
                {
                    SaveData.current.CurrentItemSHIELD = index;
                    Buttons[index].transform.DOLocalMove(SpotShield.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemSHIELD].SetActive(false);
                    Buttons[SaveData.current.CurrentItemSHIELD].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemSHIELD].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    SaveData.current.CurrentItemSHIELD = index;
                    Buttons[index].transform.DOLocalMove(SpotShield.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
            }
        }
        else if (index == 4 || index == 5 || index == 6)//AMULETTES
        {
            if (index == SaveData.current.CurrentItemAMU)
            {
                SaveData.current.CurrentItemAMU = -1;
                Buttons[index].transform.DOLocalMove(SpotsOFF[index].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                Illus[index].SetActive(false);
            }
            else
            {
                if (SaveData.current.CurrentItemAMU == -1)
                {
                    SaveData.current.CurrentItemAMU = index;
                    Buttons[index].transform.DOLocalMove(SpotAmu.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
                else
                {
                    Illus[SaveData.current.CurrentItemAMU].SetActive(false);
                    Buttons[SaveData.current.CurrentItemAMU].transform.DOLocalMove(SpotsOFF[SaveData.current.CurrentItemAMU].transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    SaveData.current.CurrentItemAMU = index;
                    Buttons[index].transform.DOLocalMove(SpotAmu.transform.localPosition, 0.3f).OnComplete(() => { ESS.PlaySound(ESS.UI_CARNET_ScotchPose, ESS.Asource_Interface, 0.8f, false); });
                    Illus[index].SetActive(true);
                }
            }
        }
        HM.UpdateClothes();
        HM.UpdateHands();
    }

    public void UNIQUES_PopItems()
    {
        if (SaveData.current.haveMAILLET)
        {
            CEINTURE_Maillet.SetActive(true);
        }
        else
        {
            CEINTURE_Maillet.SetActive(false);
        }

        if (SaveData.current.haveDiscoveredMoney)
        {
            POCHE_Money.SetActive(true);
        }
        else
        {
            POCHE_Money.SetActive(false);
        }

        if (SaveData.current.haveDiscoveredCandy)
        {
            POCHE_Candy.SetActive(true);
        }
        else
        {
            POCHE_Candy.SetActive(false);
        }

        if (SaveData.current.haveDiscoveredYellowKey)
        {
            POCHE_YellowKey.SetActive(true);
        }
        else
        {
            POCHE_YellowKey.SetActive(false);
        }
    }

    public void CODEX_Setup()
    {
        for(int i = 0; i < CODEX_Bestiaire_List.Length; i++)
        {
            if(i == CODEX_Bestiaire_CurrentIndex - 1)
            {
                if (CODEX_Bestiaire_List[i].gameObject.activeSelf)
                {
                    CODEX_Bestiaire_List[i].GoLeft();
                }
                else
                {
                    CODEX_Bestiaire_List[i].gameObject.SetActive(true);
                    CODEX_Bestiaire_List[i].PopLeft();
                }
            }
            else if(i == CODEX_Bestiaire_CurrentIndex)
            {
                if (CODEX_Bestiaire_List[i].gameObject.activeSelf)
                {
                    CODEX_Bestiaire_List[i].GoMid();
                }
                else
                {
                    CODEX_Bestiaire_List[i].gameObject.SetActive(true);
                    CODEX_Bestiaire_List[i].PopMid();
                }
            }
            else if (i == CODEX_Bestiaire_CurrentIndex +1)
            {
                if (CODEX_Bestiaire_List[i].gameObject.activeSelf)
                {
                    CODEX_Bestiaire_List[i].GoRight();
                }
                else
                {
                    CODEX_Bestiaire_List[i].gameObject.SetActive(true);
                    CODEX_Bestiaire_List[i].PopRight();
                }
            }
            else
            {
                CODEX_Bestiaire_List[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < CODEX_Lieux_List.Length; i++)
        {
            if (i == CODEX_Lieux_CurrentIndex - 1)
            {
                if (CODEX_Lieux_List[i].gameObject.activeSelf)
                {
                    CODEX_Lieux_List[i].GoLeft();
                }
                else
                {
                    CODEX_Lieux_List[i].gameObject.SetActive(true);
                    CODEX_Lieux_List[i].PopLeft();
                }
            }
            else if (i == CODEX_Lieux_CurrentIndex)
            {
                if (CODEX_Lieux_List[i].gameObject.activeSelf)
                {
                    CODEX_Lieux_List[i].GoMid();
                }
                else
                {
                    CODEX_Lieux_List[i].gameObject.SetActive(true);
                    CODEX_Lieux_List[i].PopMid();
                }
            }
            else if (i == CODEX_Lieux_CurrentIndex + 1)
            {
                if (CODEX_Lieux_List[i].gameObject.activeSelf)
                {
                    CODEX_Lieux_List[i].GoRight();
                }
                else
                {
                    CODEX_Lieux_List[i].gameObject.SetActive(true);
                    CODEX_Lieux_List[i].PopRight();
                }
            }
            else
            {
                CODEX_Lieux_List[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < CODEX_Souvenirs_List.Length; i++)
        {
            if (i == CODEX_Souvenirs_CurrentIndex - 1)
            {
                if (CODEX_Souvenirs_List[i].gameObject.activeSelf)
                {
                    CODEX_Souvenirs_List[i].GoLeft();
                }
                else
                {
                    CODEX_Souvenirs_List[i].gameObject.SetActive(true);
                    CODEX_Souvenirs_List[i].PopLeft();
                }
            }
            else if (i == CODEX_Souvenirs_CurrentIndex)
            {
                if (CODEX_Souvenirs_List[i].gameObject.activeSelf)
                {
                    CODEX_Souvenirs_List[i].GoMid();
                }
                else
                {
                    CODEX_Souvenirs_List[i].gameObject.SetActive(true);
                    CODEX_Souvenirs_List[i].PopMid();
                }
            }
            else if (i == CODEX_Souvenirs_CurrentIndex + 1)
            {
                if (CODEX_Souvenirs_List[i].gameObject.activeSelf)
                {
                    CODEX_Souvenirs_List[i].GoRight();
                }
                else
                {
                    CODEX_Souvenirs_List[i].gameObject.SetActive(true);
                    CODEX_Souvenirs_List[i].PopRight();
                }
            }
            else
            {
                CODEX_Souvenirs_List[i].gameObject.SetActive(false);
            }
        }
    }

    public void SETTINGS_OpenSecondaryMenu(int index)
    {
        SETTINGS_UpdateDifficultyUI();
        SETTINGS_CurrentIndex = index;
        for(int i = 0; i < SETTINGS_SecMenuOpen.Length; i++)
        {
            SETTINGS_SecMenuButton[i].SetActive(true);
            if (i == SETTINGS_CurrentIndex)
            {
                SETTINGS_SecMenuOpen[i].SetActive(true);
                SETTINGS_SecMenuClosed[i].SetActive(false);
                //SETTINGS_SecMenuIcone[i].SetActive(true);
                SETTINGS_SecMenuButton[i].GetComponent<PopWhenSelected>().StayOn = true;
                SaveParameter.current.MMTMP.CS = MenuMemoryTMP.CancelState.OnSettingsSecondaryMenu;
            }
            else
            {
                SETTINGS_SecMenuOpen[i].SetActive(false);
                SETTINGS_SecMenuClosed[i].SetActive(true);
                //SETTINGS_SecMenuIcone[i].SetActive(false);
                SETTINGS_SecMenuButton[i].GetComponent<PopWhenSelected>().StayOn = false;
            }
        }
    }

    public void SETTINGS_TryToGoBackToMainMenu()
    {
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
        CloseMainMenu();
        Menu_ChambreOuRester.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Button_Rester);
        SaveParameter.current.canUseInputs = false;
    }

    public void SETTINGS_GoBackToMainMenu()
    {
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
        SaveData.current.ResetValueToDefault();
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 0);
        StartCoroutine(FadeNLoad());
    }

    public void SETTINGS_TryToQuitGame()
    {
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
        CloseMainMenu();
        Menu_QuitterOuRester.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Button_NePasQuitter);
        SaveParameter.current.canUseInputs = false;
    }

    public void SETTINGS_GoQuitGame()
    {
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
        Application.Quit();
    }

    public void SETTINGS_ChangeInputMode(int index)
    {
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
        SaveParameter.current.InputMode = index;
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 0);
        StartCoroutine(FadeNLoad());
    }

    public void SETTINGS_GoFullscreen(bool b)
    {
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
        Screen.fullScreen = b;
    }

    public void SETTINGS_UpdateDifficultyUI()
    {
        for (int j = 0; j < SETTINGS_DiffCards.Length; j++)
        {
            if (SaveData.current.CurrentDifficulty == j)
            {
                if (EventSystem.current.currentSelectedGameObject == SETTINGS_DiffiButtons[0] || EventSystem.current.currentSelectedGameObject == SETTINGS_DiffiButtons[1])
                {
                    SETTINGS_DiffCards[j].transform.DOKill();
                    SETTINGS_DiffCards[j].transform.DOLocalMove(SETTINGS_OpenDiffCardPOS.transform.localPosition, 0.2f);
                    ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.1f, false);
                }
                else
                {
                    SETTINGS_DiffCards[j].transform.DOKill();
                    SETTINGS_DiffCards[j].transform.DOLocalMove(SETTINGS_CloseDiffCardPOS.transform.localPosition, 0.2f);
                }
            }
            else
            {
                SETTINGS_DiffCards[j].transform.DOKill();
                SETTINGS_DiffCards[j].transform.DOLocalMove(SETTINGS_CloseDiffCardPOS.transform.localPosition, 0.2f);
            }
        }

        for (int i = 0; i < SETTINGS_DiffNames.Length; i++)
        {
            if (SaveData.current.CurrentDifficulty == i)
            {
                SETTINGS_DiffNames[i].SetActive(true);
            }
            else
            {
                SETTINGS_DiffNames[i].SetActive(false);
            }
        }
        BM.UpdateValues();
    }

    public void SETTINGS_TryUpDifficulty()
    {
        SETTINGS_OpenSecondaryMenu(-1); 
        if (SaveData.current.CurrentDifficulty < SETTINGS_DiffNames.Length - 1)
        {
            ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
            SaveData.current.CurrentDifficulty++;
            SETTINGS_UpdateDifficultyUI();
        }
        else
        {
            ESS.PlaySound(ESS.UI_Annuler, ESS.Asource_Interface, 0.8f, false);
        }
    }

    public void SETTINGS_TryDownDifficulty()
    {
        SETTINGS_OpenSecondaryMenu(-1);
        if (SaveData.current.CurrentDifficulty > 0)
        {
            ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
            SaveData.current.CurrentDifficulty--;
            SETTINGS_UpdateDifficultyUI();
        }
        else
        {
            ESS.PlaySound(ESS.UI_Annuler, ESS.Asource_Interface, 0.8f, false);
        }
    }

    public void OpenOrKillMainMenu()
    {
        if (!isDialogueOn && !Menu_ContinueOuNouvelle.activeSelf && !Menu_EcraserOuAnnuler.activeSelf && !CM.inCinematic() && !TM.isInTuto())// Verif si autre UI n'est pas deja affichée
        {
            if (!MenuOn )
            {
                ESS.PlaySound(ESS.UI_CARNET_OuvertureCarnet, ESS.Asource_Interface, 0.8f, false);
                SaveParameter.current.canUseInputs = false;
                MenuOn = true;
                Carnet_GLOBAL.SetActive(true);
                if (Carnet_OPEN.activeSelf)
                {
                    OpenPage(SaveParameter.current.MMTMP.index);
                    if (SaveParameter.current.MMTMP.index == 2)
                    {
                        STUFF_PopButton();
                    }
                    EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu);
                    Carnet_GLOBAL.transform.DOKill();
                    Carnet_GLOBAL.transform.DOScale(Carnet_ORIGIN_scale, 0.19f);
                    Carnet_GLOBAL.transform.DOLocalMove(Carnet_ORIGIN, 0.2f);
                }
                else
                {
                    Carnet_GLOBAL.transform.DOKill();
                    Carnet_GLOBAL.transform.DOScale(Carnet_ORIGIN_scale, 0.39f);
                    Carnet_GLOBAL.transform.DOLocalMove(Carnet_ORIGIN, 0.6f).OnComplete(() => { Carnet_OPEN.SetActive(true); OpenPage(0); EventSystem.current.SetSelectedGameObject(ButtonFirstMainMenu); });
                }
            }
            else
            {
                ESS.PlaySound(ESS.UI_CARNET_FermetureCarnet, ESS.Asource_Interface, 0.8f, false);
                SaveParameter.current.canUseInputs = true;
                MenuOn = false;
                Carnet_GLOBAL.transform.DOKill();
                Carnet_GLOBAL.transform.DOScale(Carnet_HiddenPos.localScale, 0.19f);
                Carnet_GLOBAL.transform.DOLocalMove(Carnet_HiddenPos.localPosition, 0.2f).OnComplete(() => { Carnet_GLOBAL.SetActive(false); });
            }
        }
        
    }

    public void CloseMainMenu()
    {
        ESS.PlaySound(ESS.UI_CARNET_FermetureCarnet, ESS.Asource_Interface, 0.8f, false);
        
        SaveParameter.current.canUseInputs = true;
        Carnet_OPEN.SetActive(false);
        MenuOn = false;
        Carnet_GLOBAL.transform.DOKill();
        Carnet_GLOBAL.transform.DOScale(Carnet_HiddenPos.localScale, 0.29f);
        Carnet_GLOBAL.transform.DOLocalMove(Carnet_HiddenPos.localPosition, 0.3f).OnComplete(() => { Carnet_GLOBAL.SetActive(false); });
    }

    public void QuitAllMenu()
    {
        ESS.PlaySound(ESS.UI_Annuler, ESS.Asource_Interface, 0.8f, false);
        Menu_ContinueOuNouvelle.SetActive(false);
        Menu_EcraserOuAnnuler.SetActive(false);
        Menu_QuitterOuRester.SetActive(false);
        Menu_ChambreOuRester.SetActive(false);
        SaveParameter.current.canUseInputs = true;
    }

    //--------------------  DIALOGUE  -------------------------------------------------------------------------------------------

    public bool isInDialogue()
    {
        return isDialogueOn;
    }

    public void DIALOGUE_OpenDialogue(ConversationInfo ConvInfo)
    {
        CurrentConv = ConvInfo;
        DialogueCurrentText = "";
        ConvCurrentIndex = 0;
        BranchCurrentIndex = 0;
        SaveParameter.current.canUseInputs = false;
        isDialogueOn = true;
        DIALOGUE_InitDialogueP1();
    }

    public void DIALOGUE_InitDialogueP1()
    {
        ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.8f, false);
        ClosedScroll.SetActive(true);
        ClosedScroll.transform.DOLocalMove(CLOSED_SCROLL_ORIGIN, 0.2f).OnComplete(() => { DIALOGUE_InitDialogueP2(); });
    }

    public void DIALOGUE_InitDialogueP2()
    {
        ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.8f, false);
        ClosedScroll.transform.localPosition = CLOSED_SCROLL_HIDDEN_POS.transform.localPosition;
        ClosedScroll.SetActive(false);
        RollingScroll.SetActive(true);
        RollingScroll.transform.DOScale(new Vector3(RollingScroll.transform.localScale.x+0.3f, RollingScroll.transform.localScale.y, RollingScroll.transform.localScale.z), 0.2f).OnComplete(() => { DIALOGUE_InitDialogueP3(); });
    }
    public void DIALOGUE_InitDialogueP3()
    {
        isUnfadingForDialogue = false;
        StartCoroutine(FadeForDialogue());
        ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminDeroule), ESS.Asource_Interface, 0.8f, false);
        RollingScroll.transform.localScale = new Vector3(RollingScroll.transform.localScale.x - 0.3f, RollingScroll.transform.localScale.y, RollingScroll.transform.localScale.z);
        RollingScroll.SetActive(false);
        FlatScroll.SetActive(true);
        FlatScroll.transform.localPosition = FLAT_SCROLL_ORIGIN;
        DIALOGUE_UpdateCharacter();
        DIALOGUE_UpdateText();
        DIALOGUE_UpdateButton();
    }

    public void DIALOGUE_UpdateText()
    {
        DialogueCurrentText = "";
        DIALOGUE_TextContent.text = "";
        isWritingDialogue = true;
    }

    public void DIALOGUE_UpdateButton()
    {
        DIALOGUE_ButtonA.SetActive(true);
        DIALOGUE_ButtonAtext.text = CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ButtonAContent;
        if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].isButtonBPresent)
        {
            DIALOGUE_ButtonB.SetActive(true);
            DIALOGUE_ButtonBtext.text = CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ButtonBContent;
        }
        else
        {
            DIALOGUE_ButtonB.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(DIALOGUE_ButtonA);
    }

    public void DIALOGUE_UpdateCharacter()
    {
        foreach(DialogueCharaInfo dci in CharactersOnLeft)
        {
            if(CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].PersoLeft == dci.Name)
            {
                if (!dci.gameObject.activeSelf)
                {
                    dci.gameObject.SetActive(true);
                    dci.Wake();
                }

                dci.PopEmotion(CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].PersoLeftEmotionIndex);

                if(CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].WhoIsTalking == WIT.Left || CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].WhoIsTalking == WIT.Both)
                {
                    dci.Talk();
                }
                else
                {
                    dci.StopTalking();
                }
            }
            else
            {
                if (dci.gameObject.activeSelf)
                {
                    dci.StopTalking();
                    dci.PutToSleep();
                }
            }
        }

        foreach (DialogueCharaInfo dci in CharactersOnRight)
        {
            if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].PersoRight == dci.Name)
            {
                if (!dci.gameObject.activeSelf)
                {
                    dci.gameObject.SetActive(true);
                    dci.Wake();
                }

                dci.PopEmotion(CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].PersoRightEmotionIndex);

                if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].WhoIsTalking == WIT.Right || CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].WhoIsTalking == WIT.Both)
                {
                    dci.Talk();
                }
                else
                {
                    dci.StopTalking();
                }
            }
            else
            {
                if (dci.gameObject.activeSelf)
                {
                    dci.StopTalking();
                    dci.PutToSleep();
                }
            }
        }
    }

    public void DIALOGUE_KILLUI()
    {
        isUnfadingForDialogue = true;
        StartCoroutine(UnfadeForDialogue());
        ESS.PlaySound(ESS.OneOf(ESS.UI_DIALOGUE_ParcheminEnroule), ESS.Asource_Interface, 0.8f, false);
        SaveParameter.current.canUseInputs = true;
        isDialogueOn = false;
        foreach (DialogueCharaInfo dci in CharactersOnRight)
        {
            dci.PutToSleep();
        }
        foreach (DialogueCharaInfo dci in CharactersOnLeft)
        {
            dci.PutToSleep();
        }

        DIALOGUE_ButtonA.SetActive(false);
        DIALOGUE_ButtonB.SetActive(false);

        FlatScroll.transform.DOLocalMove(FLAT_SCROLL_HIDDEN_POS.transform.localPosition, 0.3f).OnComplete(() => { FlatScroll.SetActive(false);   }); ;
    }


    public void DIALOGUE_ButtonAPressed()
    {
        if (isDialogueOn)
        {
            ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
            if (isWritingDialogue)
            {
                isWritingDialogue = false;
                DIALOGUE_TextContent.text = CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].LineContent;
                return;
            }

            if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ActionButtonA == DialogueActionButton.Next)
            {
                ConvCurrentIndex++;
                DIALOGUE_UpdateCharacter();
                DIALOGUE_UpdateText();
                DIALOGUE_UpdateButton();
            }
            else if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ActionButtonA == DialogueActionButton.Quit)
            {
                DIALOGUE_KILLUI();
            }
            else if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ActionButtonA == DialogueActionButton.CinematicNextStep)
            {
                DIALOGUE_KILLUI();
                CM.LaunchCurrentStep();
            }
            else if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ActionButtonA == DialogueActionButton.Branch)
            {
                BranchCurrentIndex = CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].branchIndex;
                ConvCurrentIndex = 0;
                DIALOGUE_UpdateCharacter();
                DIALOGUE_UpdateText();
                DIALOGUE_UpdateButton();
            }
        }
        
    }

    public void DIALOGUE_ButtonBPressed()
    {
        if (isDialogueOn)
        {
            ESS.PlaySound(ESS.UI_Annuler, ESS.Asource_Interface, 0.8f, false);
            if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ActionButtonB == DialogueActionButton.Quit)
            {
                DIALOGUE_KILLUI();
            }
            else if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ActionButtonB == DialogueActionButton.Branch)
            {
                BranchCurrentIndex = CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].branchIndex;
                ConvCurrentIndex = 0;
                DIALOGUE_UpdateCharacter();
                DIALOGUE_UpdateText();
                DIALOGUE_UpdateButton();
            }
        }
    }


    // ---------------------   BLACK SCREEN   --------------------------------------------------------

    public IEnumerator Fade()
    {
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 0);
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
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 0);
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

    public IEnumerator Unfade()
    {
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 1);
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

    public IEnumerator FadeForDialogue()
    {
        BlackScreenForDialogue.gameObject.SetActive(true);
        BlackScreenForDialogue.color = new Color(0, 0, 0, 0);
        while (BlackScreenForDialogue.color.a < MaxAlphaValue || isUnfadingForDialogue)
        {
            if (BlackScreenForDialogue.color.a + Speed * Time.deltaTime >= MaxAlphaValue)
            {
                BlackScreenForDialogue.color = new Color(0, 0, 0, MaxAlphaValue);
            }
            else
            {
                BlackScreenForDialogue.color = new Color(0, 0, 0, BlackScreenForDialogue.color.a + Speed*2 * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator UnfadeForDialogue()
    {
        BlackScreenForDialogue.gameObject.SetActive(true);
        BlackScreenForDialogue.color = new Color(0, 0, 0, MaxAlphaValue);
        while (BlackScreenForDialogue.color.a > 0)
        {
            isUnfadingForDialogue = true;
            if (BlackScreenForDialogue.color.a + Speed * Time.deltaTime <= 0)
            {
                BlackScreenForDialogue.color = new Color(0, 0, 0, 0);
            }
            else
            {
                BlackScreenForDialogue.color = new Color(0, 0, 0, BlackScreenForDialogue.color.a - Speed*2 * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        isUnfadingForDialogue = false;
    }

    public bool isCarnetOn()
    {
        return MenuOn;
    }
}
