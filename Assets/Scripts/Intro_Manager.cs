using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using Cinemachine;
using System;

public class Intro_Manager : MonoBehaviour
{
    private Intro_Phase[] Phase_List;
    public CinemachineFreeLook c_VirtualCamera;
    public Image BlackScreen;
    public float Speed;
    public Text timer;
    public AudioSource AsourceMusique;

    void Start()
    {
        GameObject[] TMPlist = GameObject.FindGameObjectsWithTag("PhaseIntro");
        Phase_List = new Intro_Phase[TMPlist.Length];
        for(int i =0; i< TMPlist.Length; i++)
        {
            Phase_List[i] = TMPlist[i].GetComponent<Intro_Phase>();
        }
    }

    void Update()
    {
        foreach(Intro_Phase ip in Phase_List)
        {
            if(!ip.Done && ip.Date <= Time.time)
            {
                DoThisPhase(ip);
            }
        }

        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time);
        timer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    public void DoThisPhase(Intro_Phase ip)
    {
        ip.Done = true;

        if(ip.ActualType == Intro_Phase.Type.MoveObject)
        {
            ip.Ref1.transform.DOMove(ip.Ref2.transform.position,ip.FloatRef);
            ip.Ref1.transform.DORotate(ip.Ref2.transform.localRotation.eulerAngles, ip.FloatRef);
        }
        else if (ip.ActualType == Intro_Phase.Type.Depop)
        {
            ip.Ref1.SetActive(false);
        }
        else if (ip.ActualType == Intro_Phase.Type.Pop)
        {
            ip.Ref1.SetActive(true);
        }
        else if (ip.ActualType == Intro_Phase.Type.ChangeCamTarget)
        {
            c_VirtualCamera.m_LookAt = ip.Ref1.transform;
        }
        else if (ip.ActualType == Intro_Phase.Type.PlaySound)
        {
            ip.AudioSourceRef.PlayOneShot(ip.AudioClipRef, ip.FloatRef);
        }
        else if (ip.ActualType == Intro_Phase.Type.GoToMenu)
        {
            SceneManager.LoadScene("Loading");
        }
        else if (ip.ActualType == Intro_Phase.Type.Fade)
        {
            Speed = ip.FloatRef;
            StartCoroutine(Fade());
        }
        else if (ip.ActualType == Intro_Phase.Type.Unfade)
        {
            Speed = ip.FloatRef;
            StartCoroutine(Unfade());
        }
        else if (ip.ActualType == Intro_Phase.Type.FadeAudioNVideo)
        {
            Speed = ip.FloatRef;
            StartCoroutine(FadeAudioNVideo());
        }
    }

    public IEnumerator Fade()
    {
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 0);
        while (BlackScreen.color.a < 1)
        {
            if (BlackScreen.color.a + Speed * Time.deltaTime >= 1)
            {
                BlackScreen.color = new Color(0, 0, 0, 1);
            }
            else
            {
                BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a + Speed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator Unfade()
    {
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 1);
        while (BlackScreen.color.a > 0)
        {
            if (BlackScreen.color.a + Speed * Time.deltaTime <= 0)
            {
                BlackScreen.color = new Color(0, 0, 0, 0);
            }
            else
            {
                BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a - Speed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeAudioNVideo()
    {
        BlackScreen.gameObject.SetActive(true);
        BlackScreen.color = new Color(0, 0, 0, 0);
        while (BlackScreen.color.a < 1)
        {
            if (BlackScreen.color.a + Speed * Time.deltaTime >= 1)
            {
                BlackScreen.color = new Color(0, 0, 0, 1);
            }
            else
            {
                BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a + Speed * Time.deltaTime);
            }
            AsourceMusique.volume = 1 - BlackScreen.color.a;

            yield return new WaitForEndOfFrame();
        }
    }
}
