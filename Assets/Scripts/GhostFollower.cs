using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollower : MonoBehaviour
{
    public PlayerMovement PM;
    public float maxHeightGap;
    public float speed;
    public float maxDistFollow;

    private float GroundHeight;
    private float DefaultSpeed;

    void Start()
    {
        GroundHeight = PM.transform.position.y;
        DefaultSpeed = speed;
    }

    void Update()
    {
        Vector3 DirPos = new Vector3(PM.transform.position.x, transform.position.y, PM.transform.position.z);
        
        if (PM.IsGrounded())
        {
            GroundHeight = PM.transform.position.y;
        }

        if (Vector3.Distance(transform.position, PM.transform.position) > maxDistFollow)
        {
            speed = DefaultSpeed +(Vector3.Distance(transform.position, PM.transform.position)-maxDistFollow);
        }
        else
        {
            speed = DefaultSpeed;
        }

        if(Vector3.Distance(DirPos, PM.transform.position) > maxHeightGap)
        {
            DirPos = new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z);
        }

        if (PM.transform.position.y< transform.position.y)
        {
            DirPos = new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z);
        }

        //transform.position = Vector3.MoveTowards(transform.position, DirPos, speed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, PM.transform.position, Time.deltaTime * speed);
        transform.rotation = PM.transform.rotation;
    }
}
