using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    private List<Interactable> PossibleInteraction = new List<Interactable>();
    Movements MovementsControls;
    public GameObject InteractionDescription;
    public Text InteractionDescriptionText;

    public float rotationSpeed;
    private float lastInteractionDate;
    private Transform target;

    void Start()
    {
        InteractionDescription.SetActive(false);
        lastInteractionDate = -999;
    }

    void Update()
    {
        if (canInteract())
        {
            InteractionDescription.SetActive(true);
            InteractionDescriptionText.text = GetClosestInteraction().TextDescription;
        }
        else
        {
            InteractionDescription.SetActive(false);
        }

        if(lastInteractionDate+0.5f>= Time.time)
        {
            FaceTarget();
        }
    }

    void Awake()
    {
        MovementsControls = new Movements();
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Interact.started += ctx => TryInteract();

        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Interact.started += ctx => TryInteract();
        }

    }

    void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Interact.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Interact.Enable();
        }

    }

    void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Interact.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Interact.Disable();
        }

    }

    public bool canInteract()
    {
        bool res = false;
        foreach(Interactable i in PossibleInteraction)
        {
            res = true;
        }
        return res;
    }

    public Interactable GetClosestInteraction()
    {
        if (canInteract())
        {
            float minDist = 999999999999;
            Interactable res = null;
            foreach (Interactable i in PossibleInteraction)
            {
                if(Vector3.Distance(transform.position, i.transform.position) < minDist)
                {
                    minDist = Vector3.Distance(transform.position, i.transform.position);
                    res = i;
                }
            }

            return res;
        }
        else
        {
            return null;
        }
    }

    public void AddInteraction(Interactable i)
    {
        PossibleInteraction.Add(i);
    }

    public void DeleteInteraction(Interactable i)
    {
        PossibleInteraction.Remove(i);
    }

    public void TryInteract()
    {
        Debug.Log("TryInteract");
        if (canInteract() && SaveParameter.current.canUseInputs)
        {
            GetClosestInteraction().Interact();
            target = GetClosestInteraction().transform;
            lastInteractionDate = Time.time;
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
