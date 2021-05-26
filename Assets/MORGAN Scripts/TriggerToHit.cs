using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToHit : MonoBehaviour
{
    public GameObject objectToTrigger;
    public Material[] colorOfTrigger;
    [SerializeField]private Renderer triggerRenderer;
    public int switchPosition;

    private void Start()
    {
        switchPosition = 1;
    }

    private void Update()
    {
        switch (switchPosition)
        {
            case 1:
                triggerRenderer.material = colorOfTrigger[0];
                break;
            case 2:
                triggerRenderer.material = colorOfTrigger[1];
                break;
        }
    }

    public void HitTheTrigger()
    {
        switch (switchPosition)
        {
            case 1:
                switchPosition = 2;
                objectToTrigger.GetComponent<MonoBehaviour>().SendMessage("ActivateMe");
                break;
            case 2:
                switchPosition = 1;
                objectToTrigger.GetComponent<MonoBehaviour>().SendMessage("DeactivateMe");
                break;
        }
    }
}
