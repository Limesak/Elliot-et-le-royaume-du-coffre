using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampReplacement : MonoBehaviour
{
    [Header("Objects")]
    public GameObject ramp1;
    public GameObject ramp2;
    public GameObject Player;

    [Header("Conditions")]
    public bool playerHasSword;
    public bool monsterDefeated;
    private bool Transitioned;

    // Start is called before the first frame update
    void Start()
    {
        if (Transitioned)
        {
            DisplayRamp(ramp2);
        }
        else
        {
            DisplayRamp(ramp1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHasSword && monsterDefeated && !Transitioned)
        {
            Transitioned = true;
            MakeTransition();
        }
    }

    void DisplayRamp(GameObject targetRamp)
    {
        targetRamp.SetActive(true);
    }

    void MakeTransition()
    {
        ramp1.SetActive(false);
        ramp2.SetActive(true);
    }
}
