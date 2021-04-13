using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Movements MovementsControls;
    public AnimationManager AM;
    public AttackUseManager AUM;
    public HandManager HM;
    public float coefHighLanding;

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

    void Start()
    {
        GravityPowerStable = GravityPower;
        isStuck = false;
        wasOnGround = true;
        isJumping = false;
        isFloating = false;
        isDiving = false;
        isMoving = false;
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
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Jump.started += ctx => TryJump();
            MovementsControls.Player1.Jump.canceled += ctx => CancelJump();
            MovementsControls.Player1.Sprint.performed += ctx => TrySprint();
            //MovementsControls.Player1.Sprint.canceled += ctx => CancelSprint();
        }
    }

    public void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Move.Enable();
            MovementsControls.Player.Sprint.Enable();
            MovementsControls.Player.Jump.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Move.Enable();
            MovementsControls.Player1.Sprint.Enable();
            MovementsControls.Player1.Jump.Enable();
        }
        
    }

    public void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.Move.Disable();
            MovementsControls.Player.Sprint.Disable();
            MovementsControls.Player.Jump.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.Move.Disable();
            MovementsControls.Player1.Sprint.Disable();
            MovementsControls.Player1.Jump.Disable();
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
            controller.slopeLimit = 90;
        }
        else
        {
            
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
                

                if (IsAlmostGrounded())
                {
                    controller.slopeLimit = 45;
                }
                else
                {
                    controller.slopeLimit = 90;
                }

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
        Vector2 inputVector = Vector2.zero;
        if (SaveParameter.current.InputMode == 0)
        {
            inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            inputVector = MovementsControls.Player1.Move.ReadValue<Vector2>();
        }
        Direction = new Vector3(inputVector.x,0, inputVector.y);

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
            float targetAngle= Mathf.Atan2(Direction.x, Direction.z) *Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir =new Vector3(moveDir.x, 0, moveDir.z);

            if (lastTimeOnGround + CoyoteTime >= Time.time || IsAlmostGrounded())
            {

                if (!AUM.GetAttacking())
                {
                    if (isSprinting && SM.UseXamount(SprintStaminaCost * Time.deltaTime))
                    {
                        var emission = WalkDustCloud.emission;
                        emission.rateOverDistance = 8;

                        controller.Move(moveDir * currentSpeed * Time.deltaTime);

                        screenShakeScript.setShake(shakeSprintForce, shakeSprintDuration);
                    }
                    else
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
                if (!AUM.GetAttacking())
                {
                    if (isSprinting)
                    {
                        controller.Move(moveDir * currentSpeed * Time.deltaTime * AirControlDivFactor);
                    }
                    else
                    {
                        controller.Move(moveDir * Direction.magnitude * speed * Time.deltaTime * AirControlDivFactor);
                        isSprinting = false;
                    }
                }
                
            }
            


        }
        else
        {
            if (SaveParameter.current.InputMode == 1)
            {
                isSprinting = false;
            }
            isMoving = false ;
        }
    }

    public void TryJump()
    {
        if (lastTimeJump + JumpCD <= Time.time && SaveParameter.current.canUseInputs && !AUM.GetAttacking())
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
            if (DashLastTime + DashCoolDown <= Time.time && wasOnGround && SaveParameter.current.canUseInputs && !AUM.GetAttacking())
            {
                DashLastTime = Time.time;
                Dash();
                
            }
            isSprinting = false;
        }
        else
        {
            if ((lastTimeOnGround + CoyoteTime >= Time.time || IsAlmostGrounded()) && SaveParameter.current.canUseInputs && !AUM.GetAttacking())
            {
                if (!isSprinting)
                {
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
        return Physics.Raycast(transform.position, -Vector3.up, distToGround*2);
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
        screenShakeScript.setShake(PushDirection.magnitude*shakePushFactor, 0.25f);
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
    }

    public void Dash()
    {
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
        
    }

    public void IncJump(Vector3 dir)
    {
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
        GravityPower = DoubleJumpingPower ;
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

}
