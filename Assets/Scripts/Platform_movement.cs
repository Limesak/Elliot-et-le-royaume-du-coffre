using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_movement : MonoBehaviour
{
    public Transform Plat;
    public Transform[] Points;
    public float speed;
    private int EndTarget;
    private int CurrentTarget;
    private bool PlayerOn;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        EndTarget = 0;
        CurrentTarget = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlat();
    }

    public void MovePlat()
    {
        Vector3 DirPos = (Points[CurrentTarget].position - Plat.position).normalized;
        float currentDistance = Vector3.Distance(Plat.position, Points[CurrentTarget].position);
        if (currentDistance > (DirPos * speed * Time.deltaTime).magnitude)
        {
            Plat.position = Plat.position + (DirPos * speed * Time.deltaTime);
            if (PlayerOn)
            {
                Player.transform.position = Player.transform.position + (DirPos * speed * Time.deltaTime);
            }
        }
        else
        {
            Plat.position = Points[CurrentTarget].position;
            if (PlayerOn)
            {
                Player.transform.position = Player.transform.position + (DirPos * speed * Time.deltaTime);
            }
            SetNewTarget();
        }
        
    }

    public void SetNewTarget()
    {
        if(EndTarget==0 && CurrentTarget != 0)
        {
            CurrentTarget--;
        }
        else if (EndTarget == 0 && CurrentTarget == 0)
        {
            EndTarget = Points.Length - 1;
            CurrentTarget++;
        }
        else if (EndTarget == Points.Length - 1 && CurrentTarget != Points.Length - 1)
        {
            CurrentTarget++;
        }
        else if (EndTarget == Points.Length - 1 && CurrentTarget == Points.Length - 1)
        {
            EndTarget = 0;
            CurrentTarget--;
        }
    }

    public void SetDetection( bool b)
    {
        PlayerOn = b;
    }
}
