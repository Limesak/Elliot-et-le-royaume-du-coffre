using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_movement : OnOffMachine
{
    public Transform Plat;
    public Transform[] Points;
    public float speed;
    private int EndTarget;
    private int CurrentTarget;
    private bool PlayerOn;
    public GameObject Player;
    private LineRenderer LR;
    // Start is called before the first frame update
    void Start()
    {
        EndTarget = 0;
        CurrentTarget = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
        LR = GetComponent<LineRenderer>();
        LR.positionCount = Points.Length;
        for (int i = 0; i < Points.Length; i++)
        {
            LR.SetPosition(i, Points[i].position);
        }
        if (isOn)
        {
            Color c = new Color(0, 0, 255, 0.4f);
            LR.startColor = c;
            LR.endColor = c;
        }
        else
        {
            Color c = new Color(255, 0, 0, 0.4f);
            LR.startColor = c;
            LR.endColor = c;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlat();
        
    }

    public void MovePlat()
    {
        if (isOn)
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
        if (isOn)
        {
            Color c = new Color(0, 0, 255, 0.4f);
            LR.startColor = c;
            LR.endColor = c;
        }
        else
        {
            Color c = new Color(255, 0, 0, 0.4f);
            LR.startColor = c;
            LR.endColor = c;
        }
    }
}
