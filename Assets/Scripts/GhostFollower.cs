using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollower : MonoBehaviour
{
    public PlayerMovement PM;
    public float maxHeightGap;
    public float speed;
    public float maxDistFollow;
    public float minDistToSnap;

    private float DefaultSpeed;

    void Start()
    {
        DefaultSpeed = speed;
    }

    void Update()
    {
        //Vector3 DirPos = new Vector3(PM.transform.position.x, transform.position.y, PM.transform.position.z);

        Vector3 DirPos = new Vector3(PM.transform.position.x, transform.position.y, PM.transform.position.z) - transform.position;


        if (Vector3.Distance(transform.position, PM.transform.position) > maxDistFollow)
        {
            speed = DefaultSpeed +Mathf.Pow(Vector3.Distance(transform.position, PM.transform.position)-maxDistFollow,2);
        }
        else
        {
            speed = DefaultSpeed;
        }

        if(Mathf.Abs(transform.position.y- PM.transform.position.y) > maxHeightGap || (PM.IsAlmostGrounded() && PM.GravityPower<=0))
        {
            Debug.Log("GoingUp");
            //DirPos = new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z);
            DirPos = (new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z) - transform.position).normalized;
        }

        if (PM.transform.position.y< transform.position.y)
        {
            //DirPos = new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z);
            DirPos = (new Vector3(PM.transform.position.x, PM.transform.position.y, PM.transform.position.z) - transform.position).normalized;
        }

        //transform.position = Vector3.MoveTowards(transform.position, DirPos, speed * Time.deltaTime);

        //transform.position = Vector3.Lerp(transform.position, DirPos, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, PM.transform.position) <= minDistToSnap && PM.GetDirection().magnitude<0.1f)
        {
            transform.position = PM.transform.position;
        }
        else
        {
            if ((DirPos * speed * Time.deltaTime).magnitude< Vector3.Distance(transform.position, PM.transform.position))
            {
                transform.position = transform.position + (DirPos * speed * Time.deltaTime);
            }
            else
            {
                transform.position = PM.transform.position;
            }
            
        }

       
        transform.rotation = PM.transform.rotation;
    }
}
