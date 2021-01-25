using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollower : MonoBehaviour
{
    public PlayerMovement PM;
    public float maxHeightGap;
    public float speed;
    public float maxDistFollow;
    private float groundY;
    private float currentDistance;

    private float DefaultSpeed;
    private bool falling;

    void Start()
    {
        DefaultSpeed = speed;
        groundY = PM.transform.position.y;
        currentDistance = Vector3.Distance(transform.position, PM.transform.position);
        falling=false;
    }

    void Update()
    {
        Vector3 DirPos = (new Vector3(PM.transform.position.x, transform.position.y, PM.transform.position.z) - transform.position).normalized;
        currentDistance = Vector3.Distance(transform.position, PM.transform.position);

        if (currentDistance > maxDistFollow)
        {
            speed = DefaultSpeed +Mathf.Pow(currentDistance - maxDistFollow,2);
        }
        else
        {
            speed = DefaultSpeed;
        }

        if(PM.transform.position.y-groundY > maxHeightGap)
        {
            DirPos = (new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z) - transform.position).normalized;
            falling = false;
        }

        if (PM.transform.position.y < transform.position.y && PM.GravityPower < 0)
        {
            DirPos = (new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z) - transform.position).normalized;
            falling = true;
        }


        if (PM.IsAlmostGrounded() && PM.GravityPower <= 0 )
        {
            groundY = PM.transform.position.y;
            falling = false;
            DirPos = (new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z) - transform.position).normalized;
        }


        if ((DirPos * speed * Time.deltaTime).magnitude <= currentDistance)
        {
            transform.position = transform.position + (DirPos * speed * Time.deltaTime);
        }
        else
        {
            transform.position = PM.transform.position;
        }




        if (currentDistance > 50)
        {
            transform.position = PM.transform.position;
        }

        transform.rotation = PM.transform.rotation;
    }
}
