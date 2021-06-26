using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    [Header("Accessors")]
    public NotifManager NM;

    [Header("Compteurs")]
    private int CPT_money;
    private int CPT_candy;
    private int CPT_yellowKey;
    private int[] CPT_kills;
    public string[] CPT_Intros;
    private int CPT_currentIntro;

    [Header("Current Mission")]
    private string MISSION_content;
    private string[] MISSION_hints;

    //[Header("Buffer")]
    private string[] BUFFER_list;

    [Header("Values")]
    public char CHAR_noteNmission;
    public char CHAR_missionNhintNhint;

    void Start()
    {
        ResetDiary();
    }


    public void ResetDiary()
    {
        if (SaveData.current.hasBeenTMP)
        {
            CPT_money = SaveData.current.TMP_CPTmoney;
            CPT_candy = SaveData.current.TMP_CPTcandy;
            CPT_yellowKey = SaveData.current.TMP_CPTYellowKey;
            CPT_kills = SaveData.current.TMP_CPTkills;
            CPT_currentIntro = SaveData.current.TMP_CPTcurrentIntro;
            BUFFER_list = SaveData.current.TMP_Buffer;
            MISSION_content = SaveData.current.TMP_MISSIONcontent;
            MISSION_hints = SaveData.current.TMP_MISSIONhint;
        }
        else
        {
            CPT_money = 0;
            CPT_candy = 0;
            CPT_yellowKey = 0;
            CPT_kills = new int[2];
            CPT_currentIntro = Random.Range(0, CPT_Intros.Length);
            BUFFER_list = new string[0];

            if (SaveData.current.Diary.Length == 0)
            {
                MISSION_content = "Je devrai explorer les environs.";//Ecrire mission 1 ici !
                MISSION_hints = new string[0];//Ajouter indice ici si première mission en a besoin (mais je trouve ca pas logique)
            }
            else
            {
                string s = SaveData.current.Diary[SaveData.current.Diary.Length - 1];
                if (s.Contains(CHAR_missionNhintNhint + ""))
                {
                    MISSION_content = s.Substring(s.IndexOf(CHAR_noteNmission)+1, s.IndexOf(CHAR_missionNhintNhint)- s.IndexOf(CHAR_noteNmission)-1);
                    MISSION_hints = s.Substring(s.IndexOf(CHAR_missionNhintNhint + "")+1).Split(CHAR_missionNhintNhint);
                    //Debug.Log("s = " + s);
                    //Debug.Log("MISSION_hints[0] = " + MISSION_hints[0]);
                    //Debug.Log("MISSION_hints.Length = " + MISSION_hints.Length);
                    /*
                    string[] tmpHint = s.Substring(s.IndexOf(CHAR_missionNhintNhint + "") + 1).Split(CHAR_missionNhintNhint);
                    MISSION_hints = new string[0];
                    for(int j = 0; j < tmpHint.Length; j++)
                    {
                        if (tmpHint[j] != "")
                        {
                            string[] newList = new string[MISSION_hints.Length + 1];
                            for (int i = 0; i < MISSION_hints.Length; i++)
                            {
                                newList[i] = MISSION_hints[i];
                            }
                            newList[MISSION_hints.Length] = tmpHint[j];
                            MISSION_hints = newList;
                            Debug.Log("tmpHint[" + j + "] = " + tmpHint[j]);
                        }
                    }*/

                }
                else
                {
                    MISSION_content = s.Substring(s.IndexOf(CHAR_noteNmission) + 1);
                    MISSION_hints = new string[0];
                }
            }
        }

        
        
    }

    public string MakeMeString()
    {
        string res = "";
        
        for(int i = 0; i < BUFFER_list.Length; i++)
        {
            res = res + BUFFER_list[i];
        }

        if (DidKill())
        {
            res = res + CPT_Intros[CPT_currentIntro] + " j'ai décimé ";
            bool first = true;
            for (int i = 0; i < CPT_kills.Length; i++)
            {
                if (CPT_kills[i] > 0)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        res = res + ", ";
                    }
                    if (i == 0)
                    {
                        res = res + CPT_kills[i] + " poussierins " ;
                    }
                    else if (i == 1)
                    {
                        res = res  +CPT_kills[i] + " magifoliens novices ";
                    }
                    else if (i == 2)
                    {
                        res = res + CPT_kills[i] + " magifoliens invocateurs ";
                    }

                }
            }

            if (DidCollect())
            {
                res = res + " et j'ai récolté ";
                bool notfirst = false;
                if (CPT_money > 0)
                {
                    notfirst = true;
                    res = res + CPT_money + " pièces";
                }
                if (CPT_candy > 0)
                {
                    if (notfirst)
                    {
                        res = res + ", ";
                    }
                    notfirst = true;
                    res = res + CPT_candy + " bonbons";
                }
                if (CPT_yellowKey > 0)
                {
                    if (notfirst)
                    {
                        res = res + ", ";
                    }
                    notfirst = true;
                    res = res + CPT_yellowKey + " clés de gobelin jaunies";
                }
                res = res + ".";
            }
            else
            {
                res = res + ".";
            }
        }
        else
        {

            if (DidCollect())
            {
                res = res + CPT_Intros[CPT_currentIntro] + " j'ai récolté ";
                bool notfirst = false;
                if (CPT_money > 0)
                {
                    notfirst = true;
                    res = res + CPT_money + " pièces";
                }
                if (CPT_candy > 0)
                {
                    if (notfirst)
                    {
                        res = res + ", ";
                    }
                    notfirst = true;
                    res = res + CPT_candy + " bonbons";
                }
                if (CPT_yellowKey > 0)
                {
                    if (notfirst)
                    {
                        res = res + ", ";
                    }
                    notfirst = true;
                    res = res + CPT_yellowKey + " clés de gobelin jaunies";
                }
                res = res + ".";
            }
        }

        res = res +CHAR_noteNmission+MISSION_content;
        for(int i = 0; i < MISSION_hints.Length; i++)
        {
            res = res + CHAR_missionNhintNhint + MISSION_hints[i];
        }

        return res;
    }

    public string GetAdventureOf(int index)
    {
        string res = "";
        string s = "";

        if (index < SaveData.current.Diary.Length && SaveData.current.Diary.Length>0)
        {
            s = SaveData.current.Diary[index];
            
        }
        else
        {
            s = MakeMeString();
        }

        res = s.Substring(0, s.IndexOf(CHAR_noteNmission));

        return res;
    }

    public string GetMissionOf(int index)
    {
        string res = "";
        string s = "";

        if (index < SaveData.current.Diary.Length && index >=0)
        {
            s = SaveData.current.Diary[index];

        }
        else
        {
            s = MakeMeString();
        }

        if (s.Contains(CHAR_missionNhintNhint + ""))
        {
            
            string[] TMPhints = s.Substring(s.IndexOf(CHAR_missionNhintNhint + "") + 1).Split(CHAR_missionNhintNhint);
            res = s.Substring(s.IndexOf(CHAR_noteNmission) + 1, s.IndexOf(CHAR_missionNhintNhint)- s.IndexOf(CHAR_noteNmission) -1);
            for (int i = 0; i < TMPhints.Length; i++)
            {
                res = res + "\n" +"¤"+ TMPhints[i];
            }
        }
        else
        { 
            res = s.Substring(s.IndexOf(CHAR_noteNmission)+1);
        }

        return res;
    }

    public bool DidKill()
    {
        bool res = false;

        for(int i = 0; i < CPT_kills.Length; i++)
        {
            if (CPT_kills[i] > 0)
            {
                res = true;
            }
        }

        return res;
    }

    public bool DidCollect()
    {
        bool res = false;

        if (CPT_money > 0 ||CPT_candy >0 || CPT_yellowKey>0)
        {
            res = true;
        }

        return res;
    }

    public void SaveTMPinSaveData()
    {
        SaveData.current.TMP_Buffer = BUFFER_list;
        SaveData.current.TMP_CPTmoney = CPT_money;
        SaveData.current.TMP_CPTcandy = CPT_candy;
        SaveData.current.TMP_CPTYellowKey = CPT_yellowKey;
        SaveData.current.TMP_CPTkills = CPT_kills;
        SaveData.current.TMP_CPTcurrentIntro = CPT_currentIntro;
        SaveData.current.TMP_MISSIONcontent = MISSION_content;
        SaveData.current.TMP_MISSIONhint = MISSION_hints;
        SaveData.current.hasBeenTMP = true;
    }

    public void SaveInSaveData()
    {
        //Faire le morceau de code dans Interactable_SavePoint
        if(BUFFER_list.Length > 0 || DidCollect() || DidKill() || CheckIfNewHintOrMission())
        {
            string[] newDiary = new string[SaveData.current.Diary.Length + 1];
            for (int i = 0; i < SaveData.current.Diary.Length; i++)
            {
                newDiary[i] = SaveData.current.Diary[i];
            }
            newDiary[SaveData.current.Diary.Length] = MakeMeString();

            SaveData.current.Diary = newDiary;
            SaveData.current.hasBeenTMP = false;
            ResetDiary();
        }
    }

    private bool CheckIfNewHintOrMission()
    {
        bool res = false;
        string s = new string(SaveData.current.Diary[SaveData.current.Diary.Length - 1].ToCharArray() );
        if (!s.Contains(MISSION_content))
        {
            res = true;
            Debug.Log("Mission Changed so reset");
            return res;
        }

        if (s.Contains(CHAR_missionNhintNhint + ""))
        {
            string[] TMPhints = s.Substring(s.IndexOf(CHAR_missionNhintNhint + "") + 1).Split(CHAR_missionNhintNhint);
            if (TMPhints.Length != MISSION_hints.Length)
            {
                res = true;
                Debug.Log("Hint Changed so reset");
                return res;
            }
            else
            {
                Debug.Log("Hint last day : " + TMPhints.Length);
            }
        }
        else
        {
            if (MISSION_hints.Length>0)
            {
                res = true;
                Debug.Log("Hint Changed so reset");
                return res;
            }
        }
        return res;
    }

    public void AddAKill(int indexType)
    {
        CPT_kills[indexType] = CPT_kills[indexType] + 1;
    }

    public void AddMoney(int howMuch)
    {
        CPT_money = CPT_money + howMuch;
    }

    public void AddCandy(int howMuch)
    {
        CPT_candy = CPT_candy + howMuch;
    }

    public void AddYellowKey(int howMuch)
    {
        CPT_yellowKey = CPT_yellowKey + howMuch;
    }

    public void AddBufferEntry(string content)
    {
        string[] newList = new string[BUFFER_list.Length + 1];
        for(int i = 0; i < BUFFER_list.Length; i++)
        {
            newList[i] = BUFFER_list[i];
        }
        newList[BUFFER_list.Length] =content;
        BUFFER_list = newList;
        NM.NewNotif("Note ajouté au carnet!");
    }

    public void ChangeTheMission(string content)
    {
        MISSION_content = content;
        MISSION_hints = new string[0];
        NM.NewNotif("Mission mise à jour!");
    }

    public void AddHint(string content)
    {
        foreach(string s in MISSION_hints)
        {
            if(content== s || content =="")
            {
                return;
            }
        }
        string[] newList = new string[MISSION_hints.Length + 1];
        for (int i = 0; i < MISSION_hints.Length; i++)
        {
            newList[i] = MISSION_hints[i];
        }
        newList[MISSION_hints.Length] = content;
        MISSION_hints = newList;
        NM.NewNotif("Indice ajouté!");
    }
}
