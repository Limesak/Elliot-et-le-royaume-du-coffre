using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockableObject : MonoBehaviour
{
    public GameObject HoverSignal;

    void Start()
    {
        HoverSignal.SetActive(false);
    }

    public void Lock()
    {
        HoverSignal.SetActive(true);
    }

    public void UnLock()
    {
        HoverSignal.SetActive(false);
    }
}
