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
    private GameObject Player;
    private LineRenderer LR;
    public bool loop;
    public Platform_PlayerDetection P_PD;

    void Start()
    {
        CurrentTarget = 0;
        Player = GameObject.FindGameObjectWithTag("Player");
        LR = GetComponent<LineRenderer>();
        if (loop)
        {
            LR.positionCount = Points.Length+1;
            for (int i = 0; i < Points.Length; i++)
            {
                LR.SetPosition(i, Points[i].position);
            }
            LR.SetPosition(Points.Length, Points[0].position);
            EndTarget = Points.Length;
        }
        else
        {
            LR.positionCount = Points.Length;
            for (int i = 0; i < Points.Length; i++)
            {
                LR.SetPosition(i, Points[i].position);
            }
            EndTarget = 0;
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

    void Update()
    {
        MovePlat();
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

    public void MovePlat()
    {
        if (isOn)
        {
            Vector3 DirPos = (Points[CurrentTarget].position - Plat.position).normalized;
            float currentDistance = Vector3.Distance(Plat.position, Points[CurrentTarget].position);
            if (currentDistance > (DirPos * speed * Time.deltaTime).magnitude)
            {
                Plat.position = Plat.position + (DirPos * speed * Time.deltaTime);
                if (P_PD.PlayerDetected)
                {
                    Player.GetComponent<CharacterController>().Move(DirPos * speed * Time.deltaTime);
                }
            }
            else
            {
                Plat.position = Points[CurrentTarget].position;
                if (P_PD.PlayerDetected)
                {
                    Player.GetComponent<CharacterController>().Move(DirPos * speed * Time.deltaTime);
                }
                SetNewTarget();
            }
        }
    }

    public void SetNewTarget()
    {
        if (loop)
        {
            if (CurrentTarget < EndTarget-1)
            {
                CurrentTarget++;
            }
            else if (CurrentTarget >= EndTarget-1)
            {
                CurrentTarget = 0; ;
            }
        }
        else
        {
            if (EndTarget == 0 && CurrentTarget != 0)
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
        
    }
}
