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

    public MenuManager MM;

    void Start()
    {
        InitVariables();
    }

    void Update()
    {
        CheckForProximity();
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
            if (Vector3.Distance(Player.transform.position, CenterOfInteraction.position) <= radius)
            {
                PlayerInRange = true;
                Player.GetComponent<InteractionManager>().AddInteraction(this);
            }
        }
    }

    public virtual void Interact()
    {
        //custom on each son
        Debug.Log("This is a motherScript, please replace it with the adequat son.");
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
