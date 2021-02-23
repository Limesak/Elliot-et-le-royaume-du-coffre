using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOnCollision : MonoBehaviour
{
    public float pushPower;
    public float coolDown;
    private float lastPush;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && lastPush + coolDown <= Time.time)
        {
            lastPush = Time.time;
            Vector3 DirPos = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<PlayerMovement>().CancelJump();
            other.gameObject.GetComponent<PlayerMovement>().SetAerianDir(new Vector3(DirPos.x* pushPower, 5, DirPos.z* pushPower));
        }
    }
}
