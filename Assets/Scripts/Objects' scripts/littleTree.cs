using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleTree : MonoBehaviour
{
    private Rigidbody rb;
    public bool Hacked, Done;
    public float timeDis;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hacked && !Done)
        {
            Done = true;
            rb.isKinematic = false;
            StartCoroutine(Disapearing());
            gameObject.layer = 9;
        }
    }

    IEnumerator Disapearing()
    {
        yield return new WaitForSeconds(timeDis);
        Destroy(gameObject);
    }
}
