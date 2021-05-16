using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChecker : MonoBehaviour
{
    public enum LookType { SwordEquiped, SwordLooted, WaitAfterX, TutoEpeeDone};
    public enum TutoType { None, DeplacementCamera, EquiperObjet, UtiliserEpee };

    [Header("Global infos")]
    public LookType Type;
    public TutoType Tuto;
    public float waitDuration;
    private TutoManager TM;
    private bool locked;
    private MenuManager MM;
    public CinematicCentralizer CC;

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
        Destroy(gameObject);
    }
}
