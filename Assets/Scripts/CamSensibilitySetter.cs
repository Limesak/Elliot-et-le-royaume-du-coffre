using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSensibilitySetter : MonoBehaviour
{
    public CinemachineFreeLook vcam;

    void Update()
    {
        vcam.m_XAxis.m_MaxSpeed = SaveParameter.current.Xsensibility;
    }
}
