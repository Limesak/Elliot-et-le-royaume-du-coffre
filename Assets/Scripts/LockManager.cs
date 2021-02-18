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
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Lock.performed += ctx => TryLock();
            MovementsControls.Player.Lock.canceled += ctx => Unlock();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Lock.started += ctx => TryLock();
            MovementsControls.Player1.Lock.canceled += ctx => Debug.Log("nothing");// Unlock();
        }
       
    }

    void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Lock.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Lock.Enable();
        }
        
    }

    void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Lock.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Lock.Disable();
        }
        
    }
    

    public void TryLock()
    {
        if(SaveParameter.current.InputMode == 0)
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
                PivotLock.target = CloserObject.transform;
                vcam.m_Priority = 0;
                vcamLock.m_Priority = 10;
            }
        }
        else if(SaveParameter.current.InputMode == 1)
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
                    PivotLock.target = CloserObject.transform;
                    vcam.m_Priority = 0;
                    vcamLock.m_Priority = 10;
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
        vcam.m_Priority = 10;
        vcamLock.m_Priority = 0;
        PivotLock.target = transform;
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

   

}
