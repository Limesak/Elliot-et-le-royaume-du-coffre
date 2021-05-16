using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NotifManager : MonoBehaviour
{
    public List<NotifEntity> List;
    public GameObject NotifHolder;
    public GameObject PREFAB_Notif;

    void Start()
    {
        List = new List<NotifEntity>();
    }

    public void NewNotif(string content)
    {
        if (SaveData.current.CurrentDifficulty != 2 && SaveData.current.CurrentDifficulty != 4)
        {
            foreach (NotifEntity ne in List)
            {
                if (ne != null)
                {
                    ne.GoDown();
                }
            }
            NotifEntity newNotif = GameObject.Instantiate(PREFAB_Notif, NotifHolder.transform).GetComponent<NotifEntity>();
            newNotif.textDisplay.text = content;
            List.Add(newNotif);
        }
        
    }
}
