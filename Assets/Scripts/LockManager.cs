using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class LockManager : MonoBehaviour
{
    public CinemachineFreeLook vcam;
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
        MovementsControls.Player.Lock.performed += ctx => TryLock();

    }

    void OnEnable()
    {
        MovementsControls.Player.Lock.Enable();
    }

    void OnDisable()
    {
        MovementsControls.Player.Lock.Disable();
    }
    

    void Update()
    {
        if (inTransition)
        {
            TransitionToNewData();
        }
    }

    public void TryLock()
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
                vcam.LookAt = CloserObject.transform;
                vcam.Follow = LockedCamFollow.transform;
                PivotLock.target = CloserObject.transform;
                PassNewDataInCam(LockedValues);
                //TransitionToNewData();
                vcamColl.m_AvoidObstacles = false;
            }
        }
        

    }

    public void Unlock()
    {
        isLock = false;
        LockedObject.UnLock();
        LockedObject = null;
        vcam.LookAt = DefaultLookAt.transform;
        vcam.Follow = DefaultLookAt.transform;
        PivotLock.target = transform;
        PassNewDataInCam(DefaultValues);
        //TransitionToNewData();
        vcamColl.m_AvoidObstacles = true;
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
