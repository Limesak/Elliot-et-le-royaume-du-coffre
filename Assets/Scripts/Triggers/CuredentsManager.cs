using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CuredentsManager : MonoBehaviour
{
    private ElliotSoundSystem ESS;
    public Rigidbody[] rbList;
    public RelevantEntity RE;
    public float MaxPush;
    public float MaxTorque;
    public bool used;
    public AudioSource Asource;
    public ParticleSystem PS;
    public Rigidbody MainRB;
    public Collider MainColl;

    void Start()
    {
        used = false;
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
    }

    public void Break()
    {
        if (!used)
        {
            used = true;
            SaveData.current.RELEVANT_KeyList = SaveData.current.RELEVANT_KeyList + RE.Key + "|";
            PS.Play();
            Destroy(MainRB);
            Destroy(MainColl);
            for (int i = 0; i < rbList.Length; i++)
            {
                rbList[i].isKinematic = false;
                rbList[i].AddForce(new Vector3(Random.Range(-MaxPush, MaxPush), Random.Range(-MaxPush, MaxPush), Random.Range(-MaxPush, MaxPush)), ForceMode.Acceleration);
                rbList[i].AddTorque(new Vector3(Random.Range(-MaxTorque, MaxTorque), Random.Range(-MaxTorque, MaxTorque), Random.Range(-MaxTorque, MaxTorque)), ForceMode.Acceleration);
                Asource.PlayOneShot(ESS.OneOf(ESS.COMBAT_TapeCuredentsAvecEpee), 0.7f);
                rbList[i].gameObject.transform.DOScale(Vector3.zero, 1.5f).OnComplete(() => { Destroy(rbList[i].gameObject); });
            }
        }
        
    }
}
