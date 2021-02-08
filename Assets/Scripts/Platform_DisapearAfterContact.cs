using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform_DisapearAfterContact : MonoBehaviour
{
    enum State { Standing, Shaking, Depoping, Loading, Popping, Growing };

    public Platform_PlayerDetection P_PD;
    public Transform Plat;
    public float TimeToDepop;
    public float TimeToPop;
    public float DisapearDuration;
    private float lastContact;

    private State currentState;

    private bool stateFinished;

    void Start()
    {
        currentState = State.Standing;
        stateFinished = false;

        
    }

    void Update()
    {
        if (currentState == State.Standing && P_PD.PlayerDetected)
        {
            lastContact = Time.time;
            currentState = State.Shaking;
            Plat.DOPunchScale(new Vector3(0.06f, 0, 0.06f),TimeToDepop,10,0).OnComplete(() => { currentState = State.Depoping; });
        }
        else if (currentState == State.Depoping)
        {
            currentState = State.Loading;
            Plat.DOScale(new Vector3(0, 0, 0), DisapearDuration).OnComplete(() => { currentState = State.Loading; });
        }
        else if (currentState == State.Loading)
        {
            P_PD.PlayerDetected = false;
            Plat.gameObject.SetActive(false);
            if(lastContact + TimeToDepop + TimeToPop <= Time.time)
            {
                currentState = State.Popping;
            }
        }
        else if (currentState == State.Popping)
        {
            currentState = State.Growing;
            Plat.gameObject.SetActive(true);
            Plat.DOScale(new Vector3(1, 1, 1), DisapearDuration).OnComplete(() => { currentState = State.Standing; });
        }
    }
}
