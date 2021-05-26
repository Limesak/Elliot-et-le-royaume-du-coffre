using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopWhenSelected : MonoBehaviour
{
    private bool isSelected;
    public GameObject ObjectToPop;
    public bool StayOn;
    public GameObject OtherPossibility;

    void Start()
    {
        isSelected = false;
        StayOn = false;
        ObjectToPop.SetActive(false);
    }

    void Update()
    {
        if (OtherPossibility != null)
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject || EventSystem.current.currentSelectedGameObject == OtherPossibility)
            {
                if (!isSelected)
                {
                    isSelected = true;
                    ObjectToPop.SetActive(true);
                }
            }
            else
            {
                if (isSelected && !StayOn)
                {
                    isSelected = false;
                    ObjectToPop.SetActive(false);
                }
            }
        }
        else
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                if (!isSelected)
                {
                    isSelected = true;
                    ObjectToPop.SetActive(true);
                }
            }
            else
            {
                if (isSelected && !StayOn)
                {
                    isSelected = false;
                    ObjectToPop.SetActive(false);
                }
            }
        }
        
    }
}
