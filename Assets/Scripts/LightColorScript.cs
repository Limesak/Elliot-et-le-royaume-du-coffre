using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightColorScript : MonoBehaviour
{
    public Vector4 hackColor;
    public float multiplier;
    public Light LightToChange;

    void Update()
    {
        var light = LightToChange;
        light.color = new Color(hackColor.x, hackColor.y, hackColor.z, hackColor.w) * multiplier;
    }
}