using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    public PlayerMovement PM;
    public Transform ModelParent;

    public float FlipDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchAirAttack()
    {
        //SaveParameter.current.canUseInputs = false;
        PM.SetGravityFloating(true);
        PM.SetDiving(true);
        PM.GravityPower = 0;
        ModelParent.DOLocalRotate(new Vector3(90, 0, 0), FlipDuration/4).OnComplete(() => { LaunchAirAttackP2(); });
    }

    public void LaunchAirAttackP2()
    {
        ModelParent.DOLocalRotate(new Vector3(180, 0, 0), FlipDuration/4).OnComplete(() => { LaunchAirAttackP3(); });
    }

    public void LaunchAirAttackP3()
    {
        ModelParent.DOLocalRotate(new Vector3(270, 0, 0), FlipDuration / 4).OnComplete(() => { LaunchAirAttackP4(); });
    }

    public void LaunchAirAttackP4()
    {
        ModelParent.DOLocalRotate(new Vector3(360, 0, 0), FlipDuration / 4).OnComplete(() => { LaunchAirAttackP5(); });
    }

    public void LaunchAirAttackP5()
    {
        //SaveParameter.current.canUseInputs = false;
        PM.SetGravityFloating(false);
        PM.GravityPower = PM.DivingGravityForce;
        ModelParent.localEulerAngles = Vector3.zero;
    }
}
