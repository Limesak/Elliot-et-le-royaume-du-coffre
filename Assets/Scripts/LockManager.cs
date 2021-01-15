using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class LockManager : MonoBehaviour
{
    public CinemachineFreeLook vcam;
    public CinemachineFreeLook vcamLock;
    public CinemachineCollider vcamColl;
    public GameObject PointOfScan;
    public float radiusOfLock;
    public bool isLock;
    public bool inTransition;
    public float incrementTransition;
    private LockableObject LockedObject;
    public LookAt PivotLock;

    public GameObject LockedCamFollow;

    public GameObject DefaultLookAt;

    public FreeLookValueDoc DefaultValues;
    public FreeLookValueDoc LockedValues;

    Movements MovementsControls;
    public InputModeSelector IMS;

    void Start()
    {
        DefaultValues = new FreeLookValueDoc();
        DefaultValues.TopH = 14;
        DefaultValues.TopR = 6;
        DefaultValues.MidH = 5;
        DefaultValues.MidR = 10;
        DefaultValues.BotH = -1;
        DefaultValues.BotR = 6;

        LockedValues = new FreeLookValueDoc();
        LockedValues.TopH = 1.5f;
        LockedValues.TopR = 0f;
        LockedValues.MidH = 1f;
        LockedValues.MidR = 0f;
        LockedValues.BotH = -0.5f;
        LockedValues.BotR = 0f;

    }


    void Awake()
    {
        MovementsControls = new Movements();
        if (IMS.InputMode == 0)
        {
            MovementsControls.Player.Lock.performed += ctx => TryLock();
            MovementsControls.Player.Lock.canceled += ctx => Unlock();
        }
        else if (IMS.InputMode == 1)
        {
            MovementsControls.Player1.Lock.started += ctx => TryLock();
            MovementsControls.Player1.Lock.canceled += ctx => Debug.Log("nothing");// Unlock();
        }
       
    }

    void OnEnable()
    {
        if (IMS.InputMode == 0)
        {
            MovementsControls.Player.Lock.Enable();
        }
        else if (IMS.InputMode == 1)
        {
            MovementsControls.Player1.Lock.Enable();
        }
        
    }

    void OnDisable()
    {
        if (IMS.InputMode == 0)
        {
            MovementsControls.Player.Lock.Disable();
        }
        else if (IMS.InputMode == 1)
        {
            MovementsControls.Player1.Lock.Disable();
        }
        
    }
    

    public void TryLock()
    {
        if(IMS.InputMode == 0)
        {
            Collider[] res = Physics.OverlapSphere(PointOfScan.transform.position, radiusOfLock);
            List<GameObject> PossibleLocks = new List<GameObject>();
            for (int i = 0; i < res.Length; i++)
            {
                if (res[i].gameObject.GetComponent<LockableObject>())
                {
                    PossibleLocks.Add(res[i].gameObject);
                }
            }
            float minDist = radiusOfLock * 10000;
            GameObject CloserObject = null;
            foreach (GameObject g in PossibleLocks)
            {
                if (Vector3.Distance(g.transform.position, PointOfScan.transform.position) < minDist)
                {
                    minDist = Vector3.Distance(g.transform.position, PointOfScan.transform.position);
                    CloserObject = g;
                }
            }

            if (CloserObject != null)
            {
                isLock = true;
                LockedObject = CloserObject.GetComponent<LockableObject>();
                LockedObject.Lock();
                //vcam.LookAt = CloserObject.transform;
                //vcam.LookAt = transform;
                //vcam.Follow = LockedCamFollow.transform;
                PivotLock.target = CloserObject.transform;
                vcam.m_Priority = 0;
                vcamLock.m_Priority = 10;
                //PassNewDataInCam(LockedValues);
                //TransitionToNewData();
                //vcamColl.m_AvoidObstacles = false;
            }
        }
        else if(IMS.InputMode == 1)
        {
            if (isLock)
            {
                Unlock();
            }
            else
            {
                Collider[] res = Physics.OverlapSphere(PointOfScan.transform.position, radiusOfLock);
                List<GameObject> PossibleLocks = new List<GameObject>();
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i].gameObject.GetComponent<LockableObject>())
                    {
                        PossibleLocks.Add(res[i].gameObject);
                    }
                }
                float minDist = radiusOfLock * 10000;
                GameObject CloserObject = null;
                foreach (GameObject g in PossibleLocks)
                {
                    if (Vector3.Distance(g.transform.position, PointOfScan.transform.position) < minDist)
                    {
                        minDist = Vector3.Distance(g.transform.position, PointOfScan.transform.position);
                        CloserObject = g;
                    }
                }

                if (CloserObject != null)
                {
                    isLock = true;
                    LockedObject = CloserObject.GetComponent<LockableObject>();
                    LockedObject.Lock();
                    //vcam.LookAt = CloserObject.transform;
                    //vcam.LookAt = transform;
                    //vcam.Follow = LockedCamFollow.transform;
                    PivotLock.target = CloserObject.transform;
                    vcam.m_Priority = 0;
                    vcamLock.m_Priority = 10;
                    //PassNewDataInCam(LockedValues);
                    //TransitionToNewData();
                    //vcamColl.m_AvoidObstacles = false;
                }
            }
        }
        
    }

    public void Unlock()
    {
        Debug.Log("Unlock");
        isLock = false;
        if (LockedObject != null)
        {
            LockedObject.UnLock();
            LockedObject = null;
        }
        //vcam.LookAt = DefaultLookAt.transform;
        //vcam.Follow = DefaultLookAt.transform;
        vcam.m_Priority = 10;
        vcamLock.m_Priority = 0;
        PivotLock.target = transform;
        //PassNewDataInCam(DefaultValues);
        //TransitionToNewData();
        //vcamColl.m_AvoidObstacles = true;
    }

    public void PassNewDataInCam(FreeLookValueDoc data)
    {
        vcam.m_Orbits[0].m_Height = data.TopH;
        vcam.m_Orbits[0].m_Radius = data.TopR;
        vcam.m_Orbits[1].m_Height = data.MidH;
        vcam.m_Orbits[1].m_Radius = data.MidR;
        vcam.m_Orbits[2].m_Height = data.BotH;
        vcam.m_Orbits[2].m_Radius = data.BotR;

    }

    public void TransitionToNewData()
    {
        int check = 0;
        if (isLock)
        {
            if (vcam.m_Orbits[0].m_Height> LockedValues.TopH)
            {
                vcam.m_Orbits[0].m_Height = vcam.m_Orbits[0].m_Height - incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[0].m_Height = LockedValues.TopH;
                check++;
            }
            if (vcam.m_Orbits[0].m_Radius > LockedValues.TopR)
            {
                vcam.m_Orbits[0].m_Radius = vcam.m_Orbits[0].m_Radius - incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[0].m_Radius = LockedValues.TopR;
                check++;
            }
            if (vcam.m_Orbits[1].m_Height > LockedValues.MidH)
            {
                vcam.m_Orbits[1].m_Height = vcam.m_Orbits[0].m_Height - incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[1].m_Height = LockedValues.MidH;
                check++;
            }
            if (vcam.m_Orbits[1].m_Radius > LockedValues.MidR)
            {
                vcam.m_Orbits[1].m_Radius = vcam.m_Orbits[0].m_Radius - incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[1].m_Radius = LockedValues.MidR;
                check++;
            }
            if (vcam.m_Orbits[2].m_Height < LockedValues.BotH)
            {
                vcam.m_Orbits[2].m_Height = vcam.m_Orbits[0].m_Height + incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[2].m_Height = LockedValues.BotH;
                check++;
            }
            if (vcam.m_Orbits[2].m_Radius > LockedValues.BotR)
            {
                vcam.m_Orbits[2].m_Radius = vcam.m_Orbits[0].m_Radius - incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[2].m_Radius = LockedValues.BotR;
                check++;
            }
        }
        else
        {
            if (vcam.m_Orbits[0].m_Height < DefaultValues.TopH)
            {
                vcam.m_Orbits[0].m_Height = vcam.m_Orbits[0].m_Height + incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[0].m_Height = DefaultValues.TopH;
                check++;
            }
            if (vcam.m_Orbits[0].m_Radius < DefaultValues.TopR)
            {
                vcam.m_Orbits[0].m_Radius = vcam.m_Orbits[0].m_Radius + incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[0].m_Radius = DefaultValues.TopR;
                check++;
            }
            if (vcam.m_Orbits[1].m_Height < DefaultValues.MidH)
            {
                vcam.m_Orbits[1].m_Height = vcam.m_Orbits[0].m_Height + incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[1].m_Height = DefaultValues.MidH;
                check++;
            }
            if (vcam.m_Orbits[1].m_Radius < DefaultValues.MidR)
            {
                vcam.m_Orbits[1].m_Radius = vcam.m_Orbits[0].m_Radius + incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[1].m_Radius = DefaultValues.MidR;
                check++;
            }
            if (vcam.m_Orbits[2].m_Height > DefaultValues.BotH)
            {
                vcam.m_Orbits[2].m_Height = vcam.m_Orbits[0].m_Height - incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[2].m_Height = DefaultValues.BotH;
                check++;
            }
            if (vcam.m_Orbits[2].m_Radius < DefaultValues.BotR)
            {
                vcam.m_Orbits[2].m_Radius = vcam.m_Orbits[0].m_Radius + incrementTransition * Time.deltaTime;
            }
            else
            {
                vcam.m_Orbits[2].m_Radius = DefaultValues.BotR;
                check++;
            }
        }

        if (check >= 6)
        {
            inTransition = false;
        }
        else
        {
            inTransition = true;
        }
        
    }

}
