using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Image Full;
    public Image Back;
    public Text SceneName;

    public GameObject[] Illus;

    void Start()
    {
        string path = SceneUtility.GetScenePathByBuildIndex(SaveData.current.currentScene);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        SceneName.text = " \"" + sceneName + "\"";
        StartCoroutine(LoadAsync());

        for(int i =0;i< Illus.Length;i++)
        {
            if( i == SaveData.current.currentScene)
            {
                Illus[i].SetActive(true);
            }
            else
            {
                Illus[i].SetActive(false);
            }
        }
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation progression = SceneManager.LoadSceneAsync(SaveData.current.currentScene);

        while(progression.progress < 1)
        {
            Full.fillAmount = progression.progress;
            Full.color = new Color(255, 255, 255, 255 * progression.progress);
            //Back.color = new Color(58, 58, 58, 255 * progression.progress);


            yield return new WaitForEndOfFrame();
        }
    }
}
