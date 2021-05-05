using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LoadingSceneManager : MonoBehaviour
{
    public Image Full;
    public Image Back;
    public Text SceneName;

    public GameObject Button;
    private bool canAppear;
    private bool canGo;

    private float TMP_prog;

    public GameObject[] Illus;

    void Start()
    {
        Button.SetActive(false);
        for (int i = 0; i < Illus.Length; i++)
        {
            if (i == SaveData.current.currentScene)
            {
                Illus[i].SetActive(true);
                
            }
            else
            {
                Illus[i].SetActive(false);

            }
        }
        string path = SceneUtility.GetScenePathByBuildIndex(SaveData.current.currentScene);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        SceneName.text = " \"" + sceneName + "\"";
        StartCoroutine(LoadAsync());
    }

    private void Update()
    {
        if(TMP_prog >= 0.9f && !canAppear)
        {
            canAppear = true;
            Button.SetActive(true);
            EventSystem.current.SetSelectedGameObject(Button);
        }
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation progression = SceneManager.LoadSceneAsync(SaveData.current.currentScene);

        progression.allowSceneActivation = false;

        if (canGo)
        {
            progression.allowSceneActivation = true;
        }

        while (!progression.isDone)
        {
            TMP_prog = progression.progress;
            Full.fillAmount = progression.progress;
            Full.color = new Color(255, 255, 255, 255 * progression.progress);
            Back.color = new Color(58, 58, 58, 255 * progression.progress);
            if (canGo)
            {
                progression.allowSceneActivation = true;
            }
            yield return new WaitForEndOfFrame();

        }

        
    }

    public void LetsGo()
    {
        canGo = true;
        Debug.Log("CanGOO");
    }
}
