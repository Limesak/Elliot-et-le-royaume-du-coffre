using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private ElliotSoundSystem ESS;
    Movements MovementsControls;
    public AnimationManager AM;
    public AttackUseManager AUM;
    public HandManager HM;
    public float coefHighLanding;
    public LifeManager LM;

    public CharacterController controller;

    private Vector3 Direction;

    private bool wasOnGround;
    
    public float turnSmoothVelocity;
    public float turnSmoothTime;
    public Transform cam;
    public float distToGround;

    public StaminaManager SM;

    public float speed;
    public float speedSprint;
    private float currentSpeed;
    public float sprintAcceleration;
    public float SprintStaminaCost;
    private bool isSprinting;
    private bool isMoving;

    public LockManager LockMan;
    public float DashForce;
    public float DashCoolDown;
    private float DashLastTime;

    public float GravityPower;
    private float GravityPowerStable;
    public float GravityPullForce;
    private bool isFloating;
    private bool isDiving;
    public float DivingGravityForce;

    public float JumpingPower;
    public float DoubleJumpingPower;
    private bool DoubleJumpAvailable;
    private float lastTimeOnGround;
    public float CoyoteTime;
    public float JumpCD;
    private float lastTimeJump;
    public float JumpMaxPressTime;
    private bool isJumping;
    private Vector3 AerianDir;
    public float AirControlDivFactor;
    public float AirInertieDivFactor;

    private Vector3 hitNormal;
    private float hitDistance;
    public float frictionSlope;
    private float lastY;
    private int cptSameY;
    public int limitCptY;
    private bool isStuck;
    public float slipperySlope;
    public float slopeJumpFactor;

    public ParticleSystem WalkDustCloud;

    private Vector3 GizmoLocation;

    public float Pushfriction;
    private Vector3 PushDirection;
    private bool isPushed;
    public float PushSpeed;
    public float MinPushForce;

    public float shakeJumpForce;
    public float shakeJumpDuration;
    public float shakeSprintForce;
    public float shakeSprintDuration;
    public float shakeDashForce;
    public float shakeDashDuration;
    public float shakePushFactor;

    public ScreenShake screenShakeScript;
    public ScreenShake screenShakeScriptLock;

    private bool isBlocking;

    void Start()
    {
        GravityPowerStable = GravityPower;
        isStuck = false;
        wasOnGround = true;
        isJumping = false;
        isFloating = false;
        isDiving = false;
        isMoving = false;
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
        
    }

    void Awake()
    {
        MovementsControls = new Movements();
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Jump.started += ctx => TryJump();
            MovementsControls.Player.Jump.canceled += ctx => CancelJump();
            MovementsControls.Player.Sprint.performed += ctx => TrySprint();
            MovementsControls.Player.Sprint.canceled += ctx => CancelSprint();
            MovementsControls.Player.Block.performed += ctx => TryToBlock();
            MovementsControls.Player.Block.canceled += ctx => CancelBlock();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Jump.started += ctx => TryJump();
            MovementsControls.Player1.Jump.canceled += ctx => CancelJump();
            MovementsControls.Player1.Sprint.performed += ctx => TrySprint();
            //MovementsControls.Player1.Sprint.canceled += ctx => CancelSprint();
            MovementsControls.Player1.Block.performed += ctx => TryToBlock();
            MovementsControls.Player1.Block.canceled += ctx => CancelBlock();
        }
    }

    public void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Move.Enable();
            MovementsControls.Player.Sprint.Enable();
            MovementsControls.Player.Jump.Enable();
            MovementsControls.Player.Block.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Move.Enable();
            MovementsControls.Player1.Sprint.Enable();
            MovementsControls.Player1.Jump.Enable();
            MovementsControls.Player1.Block.Enable();
        }
        
    }

    public void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Move.Disable();
            MovementsControls.Player.Sprint.Disable();
            MovementsControls.Player.Jump.Disable();
            MovementsControls.Player.Block.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Move.Disable();
            MovementsControls.Player1.Sprint.Disable();
            MovementsControls.Player1.Jump.Disable();
            MovementsControls.Player1.Block.Disable();
        }
        
    }

    void Update()
    {
        //Gravity
        if (IsGrounded())
        {
            
            wasOnGround = true;

            lastTimeOnGround = Time.time;
            DoubleJumpAvailable = true;
            
            if (GravityPower > 0)
            {
                if (IsUnderRoof())
                {
                    GravityPower = 0;
                }
                Vector3 graviDir = new Vector3(0, GravityPower, 0);
                controller.Move(graviDir * Time.deltaTime);
            }
            else
            {
                GravityPower = 0;
                AerianDir = Vector3.zero;
            }
            isDiving = false;
            controller.slopeLimit = 45;
        }
        else
        {
            if (IsAlmostGrounded())
            {
                controller.slopeLimit = 45;
            }
            else
            {
                controller.slopeLimit = 90;
            }

            if (GravityPower > 0 && IsUnderRoof())
            {
                GravityPower = 0;
            }
            Vector3 graviDir = new Vector3(0, GravityPower, 0);
            controller.Move(graviDir * Time.deltaTime);


            if (!isJumping)
            {
                if (isDiving)
                {
                    if (GravityPower > DivingGravityForce && !isFloating)
                    {
                        GravityPower = GravityPower - (GravityPullForce * Time.deltaTime);
                    }
                    else if (GravityPower < DivingGravityForce)
                    {
                        GravityPower = DivingGravityForce;
                    }
                }
                else
                {
                    if (GravityPower > GravityPowerStable && !isFloating)
                    {
                        GravityPower = GravityPower - (GravityPullForce * Time.deltaTime);
                    }
                    else if (GravityPower < GravityPowerStable)
                    {
                        GravityPower = GravityPowerStable;
                    }
                }
                
                /*
                if (IsAlmostGrounded())
                {
                    controller.slopeLimit = 45;
                }
                else
                {
                    controller.slopeLimit = 90;
                }
                */

                if (lastY == transform.position.y && IsPossiblyStuck())
                {
                    cptSameY++;
                    if (cptSameY >= limitCptY)
                    {
                        
                        isStuck = true;
                        //lastTimeOnGround = Time.time;
                        //DoubleJumpAvailable = true;
                    }
                    else
                    {
                        //Debug.Log("HitDistance: " + hitDistance);
                    }
                }
                else
                {
                    isStuck = false;
                    lastY = transform.position.y;
                    cptSameY = 0;
                }
            }
            

            if (isStuck)
            {
                UpdateSlope();
                Vector3 SlopeDir = new Vector3((1f - hitNormal.y) * hitNormal.x * (1f - frictionSlope), 0, (1f - hitNormal.y) * hitNormal.z * (1f - frictionSlope));
                controller.Move(SlopeDir * slipperySlope * Time.deltaTime);
                DoubleJumpAvailable = true;
            }
            if (IsAlmostGrounded() )
            {
                if (GravityPower < 0)
                {
                    if (!wasOnGround)
                    {
                        //screenShakeScript.setShake(shakeJumpForce, shakeJumpDuration);
                        //landing
                        ESS.PlaySound(ESS.OneOf(ESS.MOUVEMENT_BruitDePasMarche), ESS.Asource_Effects, 0.3f, false);
                        ESS.PlaySound(ESS.OneOf(ESS.MOUVEMENT_BruitDePasSprint), ESS.Asource_Effects, 0.5f, false);
                    }
                    wasOnGround = true;
                    AerianDir = Vector3.zero;
                }
                else
                {
                    controller.Move(AerianDir * AirInertieDivFactor * Time.deltaTime);
                }
                
            }
            else
            {
                if(lastTimeJump+JumpMaxPressTime<= Time.time && wasOnGround)
                {
                    AerianDir = Vector3.zero;
                }
                wasOnGround = false;
                controller.Move(AerianDir * AirInertieDivFactor* Time.deltaTime);
            }

            
        }

        //Push
        if (isPushed)
        {
            Push();
        }

        //CheckEndOfJump
        if (isJumping && lastTimeJump + JumpMaxPressTime < Time.time)
        {
            isJumping = false;
        }


        //Movements
        if (LM.isAlive())
        {
            Vector2 inputVector = Vector2.zero;
            if (SaveParameter.current.InputMode == 0)
            {
                inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
            }
            else if (SaveParameter.current.InputMode == 1)
            {
                inputVector = MovementsControls.Player1.Move.ReadValue<Vector2>();
            }
            Direction = new Vector3(inputVector.x, 0, inputVector.y);

            if (IsAlmostGrounded())
            {
                if (isSprinting && currentSpeed < speedSprint)
                {
                    currentSpeed = currentSpeed + (sprintAcceleration * Time.deltaTime);
                }
                if (currentSpeed > speedSprint)
                {
                    currentSpeed = speedSprint;
                }
                if (!isSprinting)
                {
                    currentSpeed = speed;
                }
            }
        }
        
        

        if (IsAlmostGrounded())
        {
            WalkDustCloud.enableEmission = true;
        }
        else
        {
            WalkDustCloud.enableEmission = false;
        }

        if (Direction.magnitude >= 0.1f && SaveParameter.current.canUseInputs )
        {
            isMoving = true;

            float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (SaveParameter.current.canUseRotation)
            {
                

                if (isBlocking && LockMan.GetLockedObject()!= null)
                {
                    transform.LookAt(new Vector3(LockMan.GetLockedObject().transform.position.x, transform.position.y, LockMan.GetLockedObject().transform.position.z));
                    AM.SetXYwalkVelues(Direction.x, Direction.z);
                }
                else if (isBlocking)
                {
                    transform.LookAt(new Vector3(LockMan.PointOfScan.transform.position.x, transform.position.y, LockMan.PointOfScan.transform.position.z));
                    AM.SetXYwalkVelues(Direction.x, Direction.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
            }
            

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir =new Vector3(moveDir.x, 0, moveDir.z);

            if (SaveParameter.current.canUseInputs)
            {
                if (lastTimeOnGround + CoyoteTime >= Time.time || IsAlmostGrounded())
                {

                    
                    if (isSprinting && SM.UseXamount(SprintStaminaCost * Time.deltaTime))
                    {
                       
                        var emission = WalkDustCloud.emission;
                        emission.rateOverDistance = 8;

                        controller.Move(moveDir * currentSpeed * Time.deltaTime);

                        screenShakeScript.setShake(shakeSprintForce, shakeSprintDuration, false);
                        screenShakeScriptLock.setShake(shakeSprintForce, shakeSprintDuration, false);
                        if (AUM.GetAttacking())
                        {
                            AUM.CancelAttack();
                        }
                    }
                    else if (isBlocking)
                    {
                        if (!AUM.GetAttacking())
                        {
                            var emission = WalkDustCloud.emission;
                            emission.rateOverDistance = 3;

                            controller.Move(moveDir * Direction.magnitude * (currentSpeed*0.3f) * Time.deltaTime);
                            isSprinting = false;
                        }
                    }
                    else
                    {
                        if (!AUM.GetAttacking())
                        {
                            var emission = WalkDustCloud.emission;
                            emission.rateOverDistance = 3;

                            controller.Move(moveDir * Direction.magnitude * currentSpeed * Time.deltaTime);
                            isSprinting = false;
                        }
                        
                    }
                }
                else
                {
                    
                    if (isSprinting)
                    {
                        controller.Move(moveDir * currentSpeed * Time.deltaTime * AirControlDivFactor);
                        if (AUM.GetAttacking())
                        {
                            AUM.CancelAttack();
                        }
                    }
                    else
                    {
                        if (!AUM.GetAttacking())
                        {
                            controller.Move(moveDir * Direction.magnitude * speed * Time.deltaTime * AirControlDivFactor);
                            isSprinting = false;
                        }
                        
                    }
                }
            }
            
            


        }
        else
        {
            if (!SaveParameter.current.canUseInputs)
            {
                isSprinting = false;
            }

            if (SaveParameter.current.InputMode == 1)
            {
                isSprinting = false;
            }
            isMoving = false ;

            if (isBlocking && LockMan.GetLockedObject() != null)
            {
                transform.LookAt(new Vector3(LockMan.GetLockedObject().transform.position.x, transform.position.y, LockMan.GetLockedObject().transform.position.z));
                AM.SetXYwalkVelues(Direction.x, Direction.z);
            }
            else if (isBlocking)
            {
                transform.LookAt(new Vector3(LockMan.PointOfScan.transform.position.x, transform.position.y, LockMan.PointOfScan.transform.position.z));
                AM.SetXYwalkVelues(Direction.x, Direction.z);
            }
        }
    }

    public void TryJump()
    {
        if (AUM.GetAttacking())
        {
            AUM.CancelAttack();
        }
        if (lastTimeJump + JumpCD <= Time.time && SaveParameter.current.canUseInputs && LM.isAlive())
        {
            isJumping = true;
            
            if (lastTimeOnGround + CoyoteTime >= Time.time)
            {
                Jump();
                lastTimeJump = Time.time;
            }
            else if (wasOnGround)
            {
                UpdateSlope();
                IncJump(hitNormal.normalized);
                lastTimeJump = Time.time;
            }
            else if(DoubleJumpAvailable && !isDiving && SaveData.current.CurrentItemBACK==1)
            {
                DoubleJump();
                DoubleJumpAvailable = false;
                lastTimeJump = Time.time;
            }
        }
    }

    public void CancelJump()
    {
        isJumping = false;
    }

    public void TrySprint()
    {
        if (LockMan.isLock)
        {
            if (DashLastTime + DashCoolDown <= Time.time && wasOnGround && SaveParameter.current.canUseInputs && LM.isAlive() && IsAlmostGrounded())
            {
                if (AUM.GetAttacking())
                {
                    AUM.CancelAttack();
                }
                DashLastTime = Time.time;
                Dash();
                
            }
            isSprinting = false;
        }
        else
        {
            if ((lastTimeOnGround + CoyoteTime >= Time.time || IsAlmostGrounded()) && SaveParameter.current.canUseInputs && LM.isAlive() && !isBlocking)
            {
                if (!isSprinting)
                {
                    if (AUM.GetAttacking())
                    {
                        AUM.CancelAttack();
                    }
                    currentSpeed = speed;
                    isSprinting = true;
                    HM.CurrentHands = HandManager.Holding.Empty;
                    HM.UpdateHands();
                }

            }
        }
        
    }

    public void CancelSprint()
    {
        isSprinting = false;
    }

    public float DistanceFromGround()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, 0.6f, -Vector3.up, out hit, 100);
        if (hit.point==null)
        {
            hit.point = transform.position - new Vector3(0 ,- 100, 0);
        }
        return Vector3.Distance(transform.position, hit.point);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

    public bool IsUnderRoof()
    {
        return Physics.Raycast(transform.position, Vector3.up, distToGround);
    }

    public bool IsAlmostGrounded()
    {
        //return Physics.Raycast(transform.position, -Vector3.up, distToGround*2);

        RaycastHit hit;
        return Physics.SphereCast(transform.position, 0.49f, -Vector3.up, out hit, distToGround * 1.6f);
    }

    public bool IsPossiblyStuck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position, 0.6f, -Vector3.up,out hit, distToGround * 4);
    }

    public bool IsGroundedAnim()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position, 0.6f, -Vector3.up, out hit, distToGround* coefHighLanding);
    }

    public void UpdateSlope()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.6f, -Vector3.up, out hit, Mathf.Infinity))
        {
            hitNormal = hit.normal;
            hitDistance = Vector3.Distance(transform.position, hit.point);
            GizmoLocation = hit.point;
        }
        else
        {
            GizmoLocation = Vector3.zero;
            hitNormal = Vector3.up;
        }
    }

    public void OnDrawGizmos()
    {
        if(GizmoLocation != Vector3.zero)
        {
            Gizmos.DrawSphere(GizmoLocation, 0.5f);
        }
    }

    public bool isPushingForceObservable()
    {
        bool res = false;
        if(Mathf.Abs(PushDirection.x)>=MinPushForce || Mathf.Abs(PushDirection.z) >= MinPushForce)
        {
            res = true;
        }
        return res;
    }

    public void Push()
    {
        controller.Move(PushDirection * PushSpeed * Time.deltaTime);
        screenShakeScript.setShake(PushDirection.magnitude*shakePushFactor, 0.25f, false);
        screenShakeScriptLock.setShake(PushDirection.magnitude * shakePushFactor, 0.25f,false);
        PushDirection = PushDirection  - (PushDirection*Pushfriction*Time.time);
        if (!isPushingForceObservable())
        {
            PushDirection = Vector3.zero;
            isPushed = false;
        }
    }

    public void NewPush(Vector3 dir)
    {
        GravityPower = dir.y;
        PushDirection = new Vector3(PushDirection.x + dir.x, 0, PushDirection.z + dir.z).normalized;
        isPushed = true;
    }

    public void Jump()
    {
        //Debug.Log("Jump with dir = " + dir);
        AM.StartJump();
        Vector2 inputVector = Vector2.zero;
        if (SaveParameter.current.InputMode == 0)
        {
            inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            inputVector = MovementsControls.Player1.Move.ReadValue<Vector2>();
        }
        Vector3 Direction = new Vector3(inputVector.x, 0, inputVector.y);
        if (Direction.magnitude >= 0.1f)
        {

            if (isSprinting)
            {
                AerianDir = transform.forward * currentSpeed ;
            }
            else
            {
                AerianDir = transform.forward * Direction.magnitude * speed ;
            }
        }
        GravityPower = JumpingPower;
        ESS.PlaySound(ESS.OneOf(ESS.MOUVEMENT_Saut), ESS.Asource_Effects, 0.5f, false);
    }

    public void Dash()
    {
        if (AUM.GetAttacking())
        {
            AUM.CancelAttack();
        }
        //Debug.Log("Jump with dir = " + dir);
        Vector2 inputVector = Vector2.zero;
        if (SaveParameter.current.InputMode == 0)
        {
            inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            inputVector = MovementsControls.Player1.Move.ReadValue<Vector2>();
        }
        Vector3 Direction = new Vector3(inputVector.x, 0, inputVector.y);
        if (Direction.magnitude >= 0.1f)
        {

            AerianDir = transform.forward * DashForce;
            GravityPower = 5;
        }
        ESS.PlaySound(ESS.MOUVEMENT_Dash, ESS.Asource_Effects, 0.5f, false);
        AM.Dash();
    }

    public void IncJump(Vector3 dir)
    {
        ESS.PlaySound(ESS.OneOf(ESS.MOUVEMENT_Saut), ESS.Asource_Effects, 0.4f, false);
        AerianDir = new Vector3(dir.x, 0, dir.z) * slopeJumpFactor;
        GravityPower = dir.normalized.y * JumpingPower;
        if (dir.y > 0.6f)
        {
            Vector2 inputVector = Vector2.zero;
            if (SaveParameter.current.InputMode == 0)
            {
                inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
            }
            else if (SaveParameter.current.InputMode == 1)
            {
                inputVector = MovementsControls.Player1.Move.ReadValue<Vector2>();
            }
            Vector3 Direction = new Vector3(inputVector.x, 0, inputVector.y);
            if (Direction.magnitude >= 0.1f)
            {

                if (isSprinting)
                {
                    AerianDir = (transform.forward * currentSpeed);
                }
                else
                {
                    AerianDir =  (transform.forward * Direction.magnitude * speed);
                }
            }
        }
    }

    public void DoubleJump()
    {
        AM.StartAirJump();
        GravityPower = DoubleJumpingPower ;
        ESS.PlaySound(ESS.MOUVEMENT_DoubleSaut, ESS.Asource_Effects, 0.6f, false);
    }

    public Vector3 GetDirection()
    {
        return Direction;
    }

    public void SetAerianDir(Vector3 d)
    {
        GravityPower = d.y;
        AerianDir = new Vector3(d.x, 0, d.z);
    }

    public void SetGravityFloating(bool b)
    {
        isFloating = b;
    }

    public bool GetGravityFloating()
    {
        return isFloating;
    }

    public void SetDiving(bool b)
    {
        isDiving = b;
    }

    public bool GetDiving()
    {
        return isDiving;
    }

    public bool GetJumping()
    {
        return isJumping;
    }

    public bool GetSprinting()
    {
        return isSprinting;
    }

    public bool GetMoving()
    {
        return isMoving;
    }

    public Vector3 GetDirectionInputs()
    {
        return Direction;
    }

    public bool isPlayerJumping()
    {
        return isJumping;
    }

    public bool isCurrentlyBlocking()
    {
        return isBlocking;
    }

    public void TryToBlock()
    {
        if (IsAlmostGrounded() && SaveParameter.current.canUseInputs && LM.isAlive()  && !isSprinting  && HM.CurrentShield != HandManager.ShieldType.Empty)
        {
            if (AUM.GetAttacking())
            {
                AUM.CancelAttack();
            }
            HM.CurrentHands = HandManager.Holding.SwordShield;
            HM.UpdateHands();
            isBlocking = true;
            AM.SetBlocking(true);
            Debug.Log("Blocking");
        }
        else
        {
            Debug.Log("Can't Block");
        }
    }

    public void CancelBlock()
    {
        isBlocking = false;
        AM.SetBlocking(false);
        Debug.Log("StopBlocking");
    }
}
