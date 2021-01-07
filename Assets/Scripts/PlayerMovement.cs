using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public InputAction LeftStick;
    public InputAction Sprint;

    public CharacterController controller;
    public float speed;
    public float speedSprint;
    public float turnSmoothVelocity;
    public float turnSmoothTime;
    public Transform cam;

    public StaminaManager SM;

    public float SprintStaminaCost;
    public bool isSprinting;

    void Awake()
    {
        Sprint.performed += ctx => isSprinting = true;
    }

    void OnEnable()
    {
        LeftStick.Enable();
        Sprint.Enable();
    }

    void OnDisable()
    {
        LeftStick.Disable();
        Sprint.Disable();
    }

    void Update()
    {
        Vector2 inputVector = LeftStick.ReadValue<Vector2>();
        Vector3 Direction = new Vector3(inputVector.x,0, inputVector.y);

        if(Direction.magnitude >= 0.1f)
        {
            float targetAngle= Mathf.Atan2(Direction.x, Direction.z) *Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir =new Vector3(moveDir.x, 0, moveDir.z);

            //bool isSprinting = Sprint.ReadValue<ButtonControl>();
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
            isSprinting = false;
            
        }
    }
}
