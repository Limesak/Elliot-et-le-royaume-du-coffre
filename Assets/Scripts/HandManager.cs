using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public enum SwordType { Wood, Butter, Pin, Bone};
    public enum ShieldType { Empty, Wood };
    public enum Holding { Empty, SwordShield, Wand, Bucket};

    public Holding CurrentHands;
    public SwordType CurrentSword;
    public ShieldType CurrentShield;

    [SerializeField]
    private GameObject Sword_Wood;
    [SerializeField]
    private GameObject Sword_Butter;
    [SerializeField]
    private GameObject Sword_Pin;
    [SerializeField]
    private GameObject Sword_Bone;
    [SerializeField]
    private GameObject Shield_Wood;
    [SerializeField]
    private GameObject Utility_Wand;
    [SerializeField]
    private GameObject Special_Bucket;

    void Start()
    {
        UpdateHands();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHands()
    {
        if (CurrentHands == Holding.Empty)
        {
            Sword_Wood.SetActive(false);
            Sword_Butter.SetActive(false);
            Sword_Pin.SetActive(false);
            Sword_Bone.SetActive(false);
            //Shield_Wood.SetActive(false);
            //Utility_Wand.SetActive(false);
            //Special_Bucket.SetActive(false);
        }
        else if (CurrentHands == Holding.SwordShield)
        {
            if (CurrentSword == SwordType.Wood)
            {
                Sword_Wood.SetActive(true);
                Sword_Butter.SetActive(false);
                Sword_Pin.SetActive(false);
                Sword_Bone.SetActive(false);
            }
            else if (CurrentSword == SwordType.Butter)
            {
                Sword_Wood.SetActive(false);
                Sword_Butter.SetActive(true);
                Sword_Pin.SetActive(false);
                Sword_Bone.SetActive(false);
            }
            else if (CurrentSword == SwordType.Pin)
            {
                Sword_Wood.SetActive(false);
                Sword_Butter.SetActive(false);
                Sword_Pin.SetActive(true);
                Sword_Bone.SetActive(false);
            }
            else if (CurrentSword == SwordType.Bone)
            {
                Sword_Wood.SetActive(false);
                Sword_Butter.SetActive(false);
                Sword_Pin.SetActive(false);
                Sword_Bone.SetActive(true);
            }

            if(CurrentShield == ShieldType.Empty)
            {
                //Shield_Wood.SetActive(false);
            }
            else if (CurrentShield == ShieldType.Wood)
            {
                //Shield_Wood.SetActive(true);
            }

            //Utility_Wand.SetActive(false);
            //Special_Bucket.SetActive(false);
        }
        else if(CurrentHands == Holding.Bucket)
        {
            Sword_Wood.SetActive(false);
            Sword_Butter.SetActive(false);
            Sword_Pin.SetActive(false);
            Sword_Bone.SetActive(false);
            //Shield_Wood.SetActive(false);
            //Utility_Wand.SetActive(false);
            //Special_Bucket.SetActive(true);
        }
        else if (CurrentHands == Holding.Wand)
        {
            Sword_Wood.SetActive(false);
            Sword_Butter.SetActive(false);
            Sword_Pin.SetActive(false);
            Sword_Bone.SetActive(false);
            //Shield_Wood.SetActive(false);
            //Utility_Wand.SetActive(true);
            //Special_Bucket.SetActive(false);
        }
    }
}
