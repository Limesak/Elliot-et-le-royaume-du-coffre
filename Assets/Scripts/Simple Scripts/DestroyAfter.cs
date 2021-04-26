using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float lifeDuration;
    private float birthDate;

    void Start()
    {
        birthDate = Time.time;
    }

    void Update()
    {
        if(birthDate + lifeDuration <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}
