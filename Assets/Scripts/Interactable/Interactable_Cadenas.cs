using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interactable_Cadenas : Interactable
{
    public enum LockType { Gobelin, Confiture };
    public LockType Type;
    public GameObject Model_Base;
    public GameObject Model_Anneau;
    public RelevantEntity RE;

    public AudioSource Asource_Openning;
    public AudioSource Asource_Locked;

    public GameObject LockPOS;

    public sealed override void Interact()
    {
        if(Type == LockType.Gobelin && SaveData.current.CPT_YellowKey>0 && !used)
        {
            SaveData.current.CPT_YellowKey--;
            Unlock();
        }
        else if (Type == LockType.Confiture && SaveData.current.ItemSacocheUnique[0] && !used)
        {
            Unlock();
        }
        else if (!used)
        {
            TryButLock();
        }

    }

    private void Unlock()
    {
        used = true;
        RE.NotRelevantAnymore();
        Asource_Openning.Play();
        Model_Base.transform.DOLocalMove(Model_Base.transform.localPosition - new Vector3(0,0.3f,0),0.5f);
        Model_Anneau.transform.DOLocalMove(Model_Anneau.transform.localPosition + new Vector3(0, 0.3f, 0), 0.5f).OnComplete(() => { UnlockPart2(); });
    }

    private void UnlockPart2()
    {
        Model_Base.transform.DOScale(Vector3.zero,0.2f);
        Model_Anneau.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => { Destroy(gameObject); });
    }

    private void TryButLock()
    {
        Asource_Locked.Play();
        Model_Base.transform.DOKill();
        Model_Base.transform.localPosition = LockPOS.transform.localPosition;
        Model_Base.transform.DOLocalMove(Model_Base.transform.localPosition - new Vector3(0, 0.1f, 0), 0.2f).OnComplete(() => { Model_Base.transform.DOLocalMove(Model_Base.transform.localPosition + new Vector3(0, 0.1f, 0), 0.2f); });
    }
}
