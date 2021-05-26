using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform CenterOfInteraction;
    public float radius;
    private GameObject Player;
    private bool PlayerInRange;
    public string TextDescription;
    public Interactable_Cadenas[] PossibleLock;
    public bool used;

    public MenuManager MM;

    void Start()
    {
        InitVariables();
        CustomStart();
    }

    void Update()
    {
        CheckForProximity();
        CustomUpdate();
    }

    public void InitVariables()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MM = GameObject.FindGameObjectWithTag("CanvasUI").GetComponent<MenuManager>();
        PlayerInRange = false;
    }

    public void CheckForProximity()
    {
        if (PlayerInRange)
        {
            if (Vector3.Distance(Player.transform.position, CenterOfInteraction.position) >= radius)
            {
                PlayerInRange = false;
                Player.GetComponent<InteractionManager>().DeleteInteraction(this);
            }
        }
        else
        {
            if (Vector3.Distance(Player.transform.position, CenterOfInteraction.position) <= radius && CanInteract())
            {
                PlayerInRange = true;
                Player.GetComponent<InteractionManager>().AddInteraction(this);
            }
        }
    }

    public bool CanInteract()
    {
        bool res = true;

        if(PossibleLock != null)
        {
            for (int i = 0; i < PossibleLock.Length; i++)
            {
                if (PossibleLock[i] != null)
                {
                    res = false;
                }
            }
        }
        

        if (used)
        {
            res = false;
        }

        return res;
    }

    public virtual void Interact()
    {
        //custom on each son
        Debug.Log("This is a motherScript, please replace it with the adequat son.");
    }

    public virtual void CustomUpdate()
    {
        //custom on each son
        //Debug.Log("This is a motherScript, please replace it with the adequat son.");
    }

    public virtual void CustomStart()
    {
        //custom on each son
        //Debug.Log("This is a motherScript, please replace it with the adequat son.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(CenterOfInteraction.position, radius);
    }

    public GameObject GetPlayer()
    {
        return Player;
    }
}
