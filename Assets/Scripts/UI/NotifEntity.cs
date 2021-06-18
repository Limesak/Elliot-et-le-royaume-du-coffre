using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NotifEntity : MonoBehaviour
{
    public Text textDisplay;
    public GameObject Etiquette;
    public float OffSet;
    private int nbOfOffset;
    public float lifeDuration;

    void Start()
    {
        nbOfOffset = 0;
        GoDown();
        StartCoroutine(Step1_AferWaitingX(lifeDuration));
    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        Etiquette.transform.DOLocalMove(Etiquette.transform.localPosition + new Vector3(1000,0,0), 0.06f).OnComplete(() => { Destroy(gameObject); });
    }

    public void GoDown()
    {
        nbOfOffset++;
        transform.DOKill();
        transform.DOLocalMove(Etiquette.transform.localPosition + new Vector3(transform.localPosition.x, - (OffSet* nbOfOffset), 0), 0.03f);
    }
}
