using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeMagifolienCollider : MonoBehaviour
{
    public OrbeMagifolien OM;
    public float damage;
    private int Key;
    public GameObject PS_impact;

    // Start is called before the first frame update
    void Start()
    {
        Key = Random.Range(1, 10000);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<LifeManager>())
        {
            other.gameObject.GetComponent<LifeManager>().GetDamage(new DamageForPlayer(damage, Key, OM.gameObject, other.ClosestPointOnBounds(transform.position)));
        }
        Instantiate(PS_impact, transform.position, Quaternion.identity);
        Destroy(OM.gameObject);
    }
    */

    private void Update()
    {
        transform.LookAt(OM.GetTarget());
        transform.LookAt(transform.position + OM.GetDir());
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<LifeManager>())
        {
            collision.gameObject.GetComponent<LifeManager>().GetDamage(new DamageForPlayer(damage, Key, OM.gameObject, transform.position));
        }
        Instantiate(PS_impact, transform.position, Quaternion.identity);
        Destroy(OM.gameObject);
    }
    
}
