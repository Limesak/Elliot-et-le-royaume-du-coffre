using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Movements MovementsControls;

    public CharacterController controller;
    public float speed;
    public float speedSprint;
    public float turnSmoothVelocity;
    public float turnSmoothTime;
    public Transform cam;
    public float distToGround;

    public StaminaManager SM;

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

    void Start()
    {
        GravityPowerStable = GravityPower;
    }

    void Awake()
    {
        MovementsControls = new Movements();
        MovementsControls.Player.Jump.performed += ctx => TryJump();
        MovementsControls.Player.Sprint.performed += ctx => TrySprint();
        
    }

    void OnEnable()
    {
        MovementsControls.Player.Move.Enable();
        MovementsControls.Player.Sprint.Enable();
        MovementsControls.Player.Jump.Enable();
    }

    void OnDisable()
    {
        MovementsControls.Player.Move.Disable();
        MovementsControls.Player.Sprint.Disable();
        MovementsControls.Player.Jump.Disable();
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
                Vector3 graviDir = new Vector3(0, GravityPower, 0);
                controller.Move(graviDir * Time.deltaTime);
            }
        }
        else
        {
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
        }

       


        //Movements
        Vector2 inputVector = MovementsControls.Player.Move.ReadValue<Vector2>();
        Vector3 Direction = new Vector3(inputVector.x,0, inputVector.y);

        if(Direction.magnitude >= 0.1f)
        {
            float targetAngle= Mathf.Atan2(Direction.x, Direction.z) *Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir =new Vector3(moveDir.x, 0, moveDir.z);

            if (IsGrounded())
            {
                if (isSprinting && SM.UseXamount(SprintStaminaCost * Time.deltaTime))
                {
                    controller.Move(moveDir * speedSprint * Time.deltaTime);
                }
                else
                {
                    controller.Move(moveDir * speed * Time.deltaTime);
                    isSprinting = false;
                }
            }
            else
            {
                if (isSprinting)
                {
                    controller.Move(moveDir * speedSprint * Time.deltaTime);
                }
                else
                {
                    controller.Move(moveDir * speed * Time.deltaTime);
                    isSprinting = false;
                }
            }
            


        }
        else
        {
            isSprinting = false;
            
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
                if (DoubleJumpAvailable)
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
        if (IsGrounded())
        {
            isSprinting = true;
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }
}
