using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_SceneTransitionZone : Interactable
{
    public bool NeedInteraction;
    public int SceneIndex;
    public int NextSpawnInt;

    private bool used;

    void Start()
    {
        InitVariables();
        used = false;
        TextDescription = TextDescription +" \""+ SceneManager.GetSceneByBuildIndex(SceneIndex).name + "\"" ;
    }

    public sealed override void Interact()
    {
        if (NeedInteraction)
        {
            InternInteract();
        }
    }

    private void InternInteract()
    {
        if (!used)
        {
            used = true;
            SaveData.current.currentScene = SceneIndex;
            SaveData.current.spawnInt = NextSpawnInt;
            MM.BlackScreen.gameObject.SetActive(true);
            MM.BlackScreen.color = new Color(0, 0, 0, 0);
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        while (MM.BlackScreen.color.a < 1)
        {
            if(MM.BlackScreen.color.a + MM.Speed * Time.deltaTime >= 1)
            {
                MM.BlackScreen.color = new Color(0, 0, 0, 1);
            }
            else
            {
                MM.BlackScreen.color = new Color(0, 0, 0, MM.BlackScreen.color.a + MM.Speed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Loading");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            InternInteract();
        }
    }
}
