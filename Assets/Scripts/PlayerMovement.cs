using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Movements MovementsControls;
    public InputModeSelector IMS;

    public CharacterController controller;
    
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


    public float GravityPower;
    private float GravityPowerStable;
    public float GravityPullForce;

    public float JumpingPower;
    private bool DoubleJumpAvailable;
    private float lastTimeOnGround;
    public float CoyoteTime;
    public float JumpCD;
    private float lastTimeJump;

    private Vector3 hitNormal;
    private float hitDistance;
    public float frictionSlope;
    private float lastY;
    private int cptSameY;
    public int limitCptY;
    private bool isStuck;
    public float slipperySlope;

    public ParticleSystem WalkDustCloud;

    private Vector3 GizmoLocation;

    [Header("Unlocked Skills")]
    public bool canDoubleJump;

    void Start()
    {
        GravityPowerStable = GravityPower;
        isStuck = false;
    }

    void Awake()
    {
        MovementsControls = new Movements();
        if (IMS.InputMode == 0)
        {
            MovementsControls.Player.Jump.started += ctx => TryJump();
            MovementsControls.Player.Sprint.performed += ctx => TrySprint();
            MovementsControls.Player.Sprint.canceled += ctx => CancelSprint();
        }
        else if (IMS.InputMode == 1)
        {
            MovementsControls.Player1.Jump.started += ctx => TryJump();
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

           


        }
        else
        {
            if (GravityPower > 0 && IsUnderRoof())
            {
                GravityPower = 0;
            }
            Vector3 graviDir = new Vector3(0, GravityPower, 0);
            controller.Move(graviDir * Time.deltaTime);

            if (GravityPower > GravityPowerStable)
            {
                GravityPower = GravityPower - (GravityPullForce*Time.deltaTime);
            }
            else
            {
                GravityPower = GravityPowerStable;
            }


            if (lastY == transform.position.y)
            {
                cptSameY++;
                if (cptSameY >= limitCptY )
                {
                    isStuck = true;
                    lastTimeOnGround = Time.time;
                }
                else
                {
                    Debug.Log("HitDistance: " + hitDistance);
                }
            }
            else
            {
                isStuck = false;
                lastY = transform.position.y;
            }

            if (isStuck)
            {
                UpdateSlope();
                Vector3 SlopeDir = new Vector3((1f - hitNormal.y) * hitNormal.x * (1f - frictionSlope), 0, (1f - hitNormal.y) * hitNormal.z * (1f - frictionSlope));
                controller.Move(SlopeDir * (SlopeDir .magnitude* slipperySlope) * Time.deltaTime);
            }

            
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

            if (lastTimeOnGround + CoyoteTime >= Time.time)
            {

                
                if (isSprinting && SM.UseXamount(SprintStaminaCost * Time.deltaTime))
                {
                    var emission = WalkDustCloud.emission;
                    emission.rateOverDistance = 8;
                   
                    controller.Move(moveDir * currentSpeed * Time.deltaTime);
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
                    controller.Move(moveDir * currentSpeed * Time.deltaTime);
                }
                else
                {
                    controller.Move(moveDir * Direction.magnitude * speed * Time.deltaTime);
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
            if (lastTimeOnGround + CoyoteTime >= Time.time)
            {
                lastTimeJump = Time.time;
                GravityPower = JumpingPower;
            }
            else
            {
                if (DoubleJumpAvailable && canDoubleJump)
                {
                    Debug.Log("DoubleJump");
                    DoubleJumpAvailable = false;
                    lastTimeJump = Time.time;
                    GravityPower = JumpingPower;
                }
            }
        }
    }

    public void TrySprint()
    {
        if (lastTimeOnGround + CoyoteTime >= Time.time)
        {
            if (!isSprinting)
            {
                currentSpeed = speed;
                isSprinting = true;
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
        return Physics.Raycast(transform.position, -Vector3.up, distToGround*3);
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
        }
    }

    public void OnDrawGizmos()
    {
        if(GizmoLocation != Vector3.zero)
        {
            Gizmos.DrawSphere(GizmoLocation, 0.5f);
        }
    }
}
