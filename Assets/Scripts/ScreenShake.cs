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

    private float LensORIGIN;
    public float LensCurrentZoomModifier;

    void Start()
    {
        
        actualGain = 0;
        dateToStop = 0;
        vcam = GetComponent<CinemachineFreeLook>();
        LensORIGIN = vcam.m_Lens.FieldOfView;
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

            if (vcam.m_Lens.FieldOfView < LensORIGIN)
            {
                vcam.m_Lens.FieldOfView = LensORIGIN-((dateToStop - Time.time)*LensCurrentZoomModifier);

                if (vcam.m_Lens.FieldOfView > LensORIGIN)
                {
                    vcam.m_Lens.FieldOfView = LensORIGIN;
                }
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
            vcam.m_Lens.FieldOfView = LensORIGIN;
        }

        
    }

    public void setShake(float intensity, float duration, bool zoom)
    {
        if(intensity>= actualGain)
        {
            actualGain = intensity;
            dateToStop = Time.time + duration;
            if (zoom)
            {
                vcam.m_Lens.FieldOfView = LensORIGIN - ((dateToStop - Time.time) * LensCurrentZoomModifier);
            }
        }
    }
}
