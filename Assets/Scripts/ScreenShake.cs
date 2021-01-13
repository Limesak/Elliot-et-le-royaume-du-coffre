using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    private CinemachineFreeLook vcam;
    private float dateToStop;
    private float actualGain;
    public float slowerFactor;

    void Start()
    {
        actualGain = 0;
        dateToStop = 0;
        vcam = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (dateToStop > Time.time)
        {
            for(int i = 0; i< 3; i++)
            {
                CinemachineBasicMultiChannelPerlin CBMCP = vcam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CBMCP.m_AmplitudeGain = actualGain;
            }
            actualGain = actualGain - (slowerFactor * Time.time);
            if (actualGain < 0)
            {
                actualGain = 0;
            }


        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                CinemachineBasicMultiChannelPerlin CBMCP = vcam.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CBMCP.m_AmplitudeGain = 0;
                actualGain = 0;
            }
        }
    }

    public void setShake(float intensity, float duration)
    {
        if(intensity>= actualGain)
        {
            actualGain = intensity;
            dateToStop = Time.time + duration;
        }
    }
}
