using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeLoop : MonoBehaviour
{
    public float radius;
    public float force;
    public float loopTime;
    private float lastBlast;

    void Start()
    {
        
    }

    void Update()
    {
        if(lastBlast+loopTime<= Time.time)
        {
            lastBlast = Time.time;

            Collider[] objects = UnityEngine.Physics.OverlapSphere(transform.position, radius);
            foreach (Collider h in objects)
            {
                Rigidbody r = h.GetComponent<Rigidbody>();
                if (r != null)
                {
                    r.AddExplosionForce(force, transform.position, radius);
                }
            }
        }
        
    }
}
