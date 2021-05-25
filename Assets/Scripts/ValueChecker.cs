using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChecker : MonoBehaviour
{
    public enum LookType { SwordEquiped, SwordLooted, WaitAfterX, TutoEpeeDone, PoupouAreneDead, ReadRelevantEntity};
    public enum TutoType { None, DeplacementCamera, EquiperObjet, UtiliserEpee };
    public enum ValueType { None, CureDentTuto };

    [Header("Global infos")]
    public LookType Type;
    public TutoType Tuto;
    public ValueType Value;
    public float waitDuration;
    private TutoManager TM;
    private bool locked;
    private MenuManager MM;
    public CinematicCentralizer CC;
    public RelevantEntity RE;
    public GameObject ObjectToPop;
    public string RelevantEntityName;

    void Start()
    {
        locked = false;
        TM = GameObject.FindGameObjectWithTag("CanvasUI").GetComponent<TutoManager>();
        MM = GameObject.FindGameObjectWithTag("CanvasUI").GetComponent<MenuManager>();
        if (Type == LookType.WaitAfterX)
        {
            StartCoroutine(Step1_AferWaitingX(waitDuration));
            locked = true;
        }
    }

    void Update()
    {
        if (!locked)
        {
            if (Type == LookType.SwordEquiped && !MM.isCarnetOn() && SaveData.current.CurrentItemSWORD == 2)
            {
                StartCoroutine(Step1_AferWaitingX(waitDuration));
                locked = true;
            }
            else if (Type == LookType.SwordLooted && SaveData.current.UnlockList[2])
            {
                StartCoroutine(Step1_AferWaitingX(waitDuration));
                locked = true;
            }
            else if (Type == LookType.TutoEpeeDone && SaveData.current.TutoDone_Sword)
            {
                StartCoroutine(Step1_AferWaitingX(waitDuration));
                locked = true;
            }
            else if (Type == LookType.PoupouAreneDead && SaveData.current.Achievements_AreneTuto)
            {
                StartCoroutine(Step1_AferWaitingX(waitDuration));
                locked = true;
            }
            else if (Type == LookType.ReadRelevantEntity && SaveData.current.RELEVANT_KeyList.Contains(RelevantEntityName))
            {
                StartCoroutine(Step1_AferWaitingX(waitDuration));
                locked = true;
            }
        }
        
    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        Debug.Log("Openinning tuto");
        if(Tuto == TutoType.DeplacementCamera)
        {
            TM.Open(TM.TUTO_DeplacemenNCamera);
        }
        else if (Tuto == TutoType.EquiperObjet)
        {
            TM.Open(TM.TUTO_EquiperObjet);
        }
        else if (Tuto == TutoType.UtiliserEpee)
        {
            TM.Open(TM.TUTO_Epee);
        }
        else if (Type == LookType.TutoEpeeDone)
        {
            CC.Recept();
        }
        else if (Type == LookType.PoupouAreneDead)
        {
            ObjectToPop.SetActive(true);
        }
        else if (Value == ValueType.CureDentTuto)
        {
            SaveData.current.Achievements_CureDentTuto = true;
        }

        if (RE != null)
        {
            RE.NotRelevantAnymore();
        }
        Destroy(gameObject);
    }
}
