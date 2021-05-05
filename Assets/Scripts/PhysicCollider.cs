using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicCollider : MonoBehaviour
{
    public float Force;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
        if (collision.gameObject.tag == "PhysicEntity")
        {
            Vector3 pos = transform.position;
            Vector3 dir = (collision.gameObject.transform.position - this.transform.position ).normalized;
            dir.y = 1;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir*Force,ForceMode.Impulse);
            Debug.Log("Push");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        if (other.gameObject.tag == "PhysicEntity")
        {
            Vector3 pos = transform.position;
            Vector3 dir = (other.gameObject.transform.position - this.transform.position).normalized;
            dir.y = 1;
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir * Force, ForceMode.Impulse);
            Debug.Log("Push");
        }
    }
}
