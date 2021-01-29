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

    void Start()
    {
        SceneName.text = SceneManager.GetSceneByBuildIndex(SaveData.current.currentScene).name;
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation progression = SceneManager.LoadSceneAsync(SaveData.current.currentScene);

        while(progression.progress < 1)
        {
            Full.fillAmount = progression.progress;
            Full.color = new Color(255, 255, 255, 255 * progression.progress);
            Back.color = new Color(58, 58, 58, 255 * progression.progress);


            yield return new WaitForEndOfFrame();
        }
    }
}
