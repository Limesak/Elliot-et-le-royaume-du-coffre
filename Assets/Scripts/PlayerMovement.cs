using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Movements MovementsControls;
    public InputModeSelector IMS;

    public CharacterController controller;

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

    public LockManager LockMan;
    public float DashForce;
    public float DashCoolDown;
    private float DashLastTime;

    public float GravityPower;
    private float GravityPowerStable;
    public float GravityPullForce;

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
    }

    void Awake()
    {
        MovementsControls = new Movements();
        if (IMS.InputMode == 0)
        {
            MovementsControls.Player.Jump.started += ctx => TryJump();
            MovementsControls.Player.Jump.canceled += ctx => CancelJump();
            MovementsControls.Player.Sprint.performed += ctx => TrySprint();
            MovementsControls.Player.Sprint.canceled += ctx => CancelSprint();
        }
        else if (IMS.InputMode == 1)
        {
            MovementsControls.Player1.Jump.started += ctx => TryJump();
            MovementsControls.Player1.Jump.canceled += ctx => CancelJump();
            MovementsControls.Player1.Sprint.performed += ctx => TrySprint();
            //MovementsControls.Player1.Sprint.canceled += ctx => CancelSprint();
        }
    }

    void OnEnable()
    {
        if (IMS.InputMode == 0)
        {
            MovementsControls.Player.Move.Enable();
            MovementsControls.Player.Sprint.Enable();
            MovementsControls.Player.Jump.Enable();
        }
        else if (IMS.InputMode == 1)
        {
            MovementsControls.Player1.Move.Enable();
            MovementsControls.Player1.Sprint.Enable();
            MovementsControls.Player1.Jump.Enable();
        }
        
    }

    void OnDisable()
    {
        if (IMS.InputMode == 0)
        {
            MovementsControls.Player.Move.Disable();
            MovementsControls.Player.Sprint.Disable();
            MovementsControls.Player.Jump.Disable();
        }
        else if (IMS.InputMode == 1)
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
                if (GravityPower > GravityPowerStable)
                {
                    GravityPower = GravityPower - (GravityPullForce * Time.deltaTime);
                }
                else
                {
                    GravityPower = GravityPowerStable;
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
                        Debug.Log("Stuck ");
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
                controller.Move(SlopeDir * (SlopeDir .magnitude* slipperySlope) * Time.deltaTime);
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
        if (IMS.InputMode == 0)
        {
            inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
        }
        else if (IMS.InputMode == 1)
        {
            inputVector = MovementsControls.Player1.Move.ReadValue<Vector2>();
        }
        Vector3 Direction = new Vector3(inputVector.x,0, inputVector.y);

        if(isSprinting && currentSpeed < speedSprint)
        {
            currentSpeed = currentSpeed + (sprintAcceleration * Time.deltaTime);
        }
        if(currentSpeed > speedSprint)
        {
            currentSpeed = speedSprint;
        }

        if (IsAlmostGrounded())
        {
            WalkDustCloud.enableEmission = true;
        }
        else
        {
            WalkDustCloud.enableEmission = false;
        }

        if (Direction.magnitude >= 0.1f)
        {
            float targetAngle= Mathf.Atan2(Direction.x, Direction.z) *Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir =new Vector3(moveDir.x, 0, moveDir.z);

            if (lastTimeOnGround + CoyoteTime >= Time.time || IsAlmostGrounded())
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

                    controller.Move(moveDir * Direction.magnitude * speed * Time.deltaTime);
                    isSprinting = false;
                }
            }
            else
            {
                
                if (isSprinting)
                {
                    controller.Move(moveDir * currentSpeed * Time.deltaTime* AirControlDivFactor);
                }
                else
                {
                    controller.Move(moveDir * Direction.magnitude * speed * Time.deltaTime* AirControlDivFactor);
                    isSprinting = false;
                }
            }
            


        }
        else
        {
            if (IMS.InputMode == 1)
            {
                isSprinting = false;
            }
        }
    }

    public void TryJump()
    {
        Debug.Log("TryJump");
        if (lastTimeJump + JumpCD <= Time.time)
        {
            isJumping = true;
            if (lastTimeOnGround + CoyoteTime >= Time.time)
            {
                Debug.Log("Jump");
                Jump();
                lastTimeJump = Time.time;
            }
            else if (wasOnGround)
            {
                Debug.Log("IncJump");
                UpdateSlope();
                IncJump(hitNormal.normalized);
                lastTimeJump = Time.time;
            }
            else if(DoubleJumpAvailable)
            {
                DoubleJump();
                Debug.Log("DoubleJump");
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
            if (DashLastTime + DashCoolDown <= Time.time && wasOnGround)
            {
                DashLastTime = Time.time;
                Dash();
                
            }
            isSprinting = false;
        }
        else
        {
            if (lastTimeOnGround + CoyoteTime >= Time.time || IsAlmostGrounded())
            {
                if (!isSprinting)
                {
                    currentSpeed = speed;
                    isSprinting = true;
                }

            }
        }
        
    }

    public void CancelSprint()
    {
        isSprinting = false;
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
        Vector2 inputVector = Vector2.zero;
        if (IMS.InputMode == 0)
        {
            inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
        }
        else if (IMS.InputMode == 1)
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
        if (IMS.InputMode == 0)
        {
            inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
        }
        else if (IMS.InputMode == 1)
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
        Debug.Log("Jump with dir = " + dir);
        AerianDir = new Vector3(dir.x, 0, dir.z) * slopeJumpFactor;
        GravityPower = dir.normalized.y * JumpingPower;
        if (dir.y > 0.6f)
        {
            Vector2 inputVector = Vector2.zero;
            if (IMS.InputMode == 0)
            {
                inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
            }
            else if (IMS.InputMode == 1)
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
}
