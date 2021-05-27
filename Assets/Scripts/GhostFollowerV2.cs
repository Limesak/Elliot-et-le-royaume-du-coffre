using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollowerV2 : MonoBehaviour
{
    private GameObject PointToFollow;

    public float BorderRadius_NoMoveZone;
    public float BorderRadius_MoveCloserNormal;
    public float BorderRadius_MoveCloserQuick;

    public float Speed_Normal;

    void Start()
    {
        PointToFollow = GameObject.FindGameObjectWithTag("Player");
        transform.position = PointToFollow.transform.position;
    }

    void Update()
    {
        float Distance = Vector3.Distance(PointToFollow.transform.position, transform.position);
        Vector3 DirPos = (PointToFollow.transform.position - transform.position).normalized;

        if (Distance> BorderRadius_MoveCloserQuick)
        {
            transform.position = PointToFollow.transform.position;
        }
        else if (Distance > BorderRadius_MoveCloserNormal)
        {
            transform.position = transform.position + (DirPos * (Speed_Normal + Mathf.Pow(Distance - BorderRadius_MoveCloserNormal, 2)) * Time.deltaTime);
        }
        else if (Distance > BorderRadius_NoMoveZone)
        {
            transform.position = transform.position + (DirPos * Speed_Normal * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, BorderRadius_NoMoveZone);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, BorderRadius_MoveCloserNormal);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BorderRadius_MoveCloserQuick);
    }
}
