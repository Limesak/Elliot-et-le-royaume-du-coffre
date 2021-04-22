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
    public enum WIT { None, Left, Both, Right };
    public enum DialogueActionButton { None, Next, Pass, Quit , Branch};
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

    [Header("Main Menus UNIQUES")]
    public GameObject CEINTURE_Maillet;
    public GameObject POCHE_Money;
    public GameObject POCHE_Candy;
    public GameObject POCHE_YellowKey;

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

        if(MenuOn || Menu_ContinueOuNouvelle.activeSelf || Menu_EcraserOuAnnuler.activeSelf || isDialogueOn)
        {
            SaveParameter.current.canUseInputs = false;
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
            }
            DIALOGUE_TextContent.text = DialogueCurrentText;
        }
        else
        {
            Debug.Log("Not Writing");
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
            else if(isDialogueOn && CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].isButtonBPresent)
            {
                DIALOGUE_ButtonBPressed();
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
        else if(index == 3)
        {
            UNIQUES_PopItems();
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
        ClosedScroll.SetActive(true);
        ClosedScroll.transform.DOLocalMove(CLOSED_SCROLL_ORIGIN, 0.2f).OnComplete(() => { DIALOGUE_InitDialogueP2(); });
    }

    public void DIALOGUE_InitDialogueP2()
    {
        ClosedScroll.transform.localPosition = CLOSED_SCROLL_HIDDEN_POS.transform.localPosition;
        ClosedScroll.SetActive(false);
        RollingScroll.SetActive(true);
        RollingScroll.transform.DOScale(new Vector3(RollingScroll.transform.localScale.x+0.3f, RollingScroll.transform.localScale.y, RollingScroll.transform.localScale.z), 0.2f).OnComplete(() => { DIALOGUE_InitDialogueP3(); });
    }
    public void DIALOGUE_InitDialogueP3()
    {
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
        }
        
    }

    public void DIALOGUE_ButtonBPressed()
    {
        if (isDialogueOn)
        {
            if (CurrentConv.Branch[BranchCurrentIndex].Lines[ConvCurrentIndex].ActionButtonA == DialogueActionButton.Quit)
            {
                DIALOGUE_KILLUI();
            }
        }
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
