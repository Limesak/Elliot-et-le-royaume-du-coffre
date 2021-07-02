using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbeMagifolien : MonoBehaviour
{
    private Vector3 Target;
    private Vector3 Origin;
    public float MaxDistanceFromAxes;
    public GameObject ActualOrbe;
    public float RotationSpeed;
    public float ProjectileSpeed;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        Origin = transform.position;
        Target = GameObject.FindGameObjectWithTag("Player").transform.position;
        dir = (Target - this.transform.position).normalized;
        transform.LookAt(Target);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = transform.position + dir * ProjectileSpeed * Time.deltaTime;
        transform.LookAt(Target);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + RotationSpeed * Time.deltaTime,0, 0);

        float sinValue = Mathf.Sin(Vector3.Distance(transform.position, Target)) * MaxDistanceFromAxes;
        float cosValue = Mathf.Cos(Vector3.Distance(transform.position, Target)) * MaxDistanceFromAxes;

        ActualOrbe.transform.localPosition = new Vector3(sinValue, cosValue, sinValue);

    }
}
