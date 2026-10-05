using System;
using System.Collections;
using System.Data.Common;
using System.Runtime.Serialization.Formatters;
using NUnit.Framework.Constraints;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    //for jump
    private float jumpHeight = 0.05f;
    private float gravityValue = -9.81f;


    //Movement Related: 
    private CharacterController charController;
    public float speed = 6.0f; //starting speed
    float targetSpeed = 6.0f; //for lerp adjustments between speeds
    public float gravity = -9.8f; 
    InputAction walkAction;
    InputAction sprintAction;
    InputAction jumpAction;
    Vector3 targetRotation = new Vector3(0,0,0); 
    // bool isSprinting = false; 
    float sprintTimer = 0; 
    float sprintLimit = 5f; 
    public Vector3 movement; 
    [SerializeField] GameObject cam; //Child of camera, gets x rotation without everything else
    Vector3 forward;
    Vector3 right;
    float playerY; //for jump

    //AWAKE ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void Awake()
    {
        walkAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("JumpAction");
    }

    //START ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (jumpAction.WasPressedThisFrame() && charController.isGrounded)
        {
            Debug.Log("Jump!");
            playerY = Mathf.Sqrt(jumpHeight) * -1/16f * gravity; 

            //playerY = Mathf.Sqrt(jumpHeight * -16f * gravity); //replaced gravity with 4 and -2f
        }

    }

    // Update is called once per frame
    //FIXED UPDATES ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    void FixedUpdate() //to keep camera from looking glitchy/rough movements
    {

        // float deltaX = Input.GetAxis("Horizontal") * speed; //These two are oudated due to Input System Change
        // float deltaZ = Input.GetAxis("Vertical") * speed;

        //Sprint --------------------------------------------------------------------------
        if (sprintAction.IsPressed())
        {
            //sprint
            if (sprintTimer <= sprintLimit)
            {
                targetSpeed = 12; 
                sprintTimer += Time.deltaTime; 
            } else {targetSpeed = 6; sprintTimer = sprintLimit+1;}
        } else
        {
            if (sprintTimer > 0) {sprintTimer -= Time.deltaTime/2;} else {sprintTimer = 0;}
            //not sprint
            targetSpeed = 6; 
        }

        //Movement Speed --------------------------------------------------------------------------
        speed = Mathf.Lerp(speed, targetSpeed, 12f * Time.deltaTime); //12 is a random speed adjustment - no instant start/stops

        Vector2 walkInput = walkAction.ReadValue<Vector2>(); //gets x and y (x and z) coords based on input, -1 to 1 values
    
        forward = cam.transform.forward; //using camera reference to avoid additional rotation stuff
        right = cam.transform.right;
        
        movement = forward * walkInput.y * speed; 
        movement = movement + (right * walkInput.x * speed); 
        movement = Vector3.ClampMagnitude(movement, speed);

//        movement = Vector3.ClampMagnitude(new Vector3(walkInput.x*speed, 0, walkInput.y*speed), speed); //clamped diagonal movement to speed - using camera-independent motion 
        movement.y = gravity; //keeps gravity
        movement *= Time.deltaTime; //updates over time 
        //movement = transform.TransformDirection(movement); 


        //keeps rotation to where you last looked/moved towards
        if (!(walkInput.x == 0f && walkInput.y == 0f)) //only updates looking direction if there was input
        {
            movement.y = 0;
            targetRotation = movement;
            float rotationSpeed = 720f;       
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetRotation), rotationSpeed * Time.deltaTime);
        }

//Jump control :/  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        playerY += gravity/7 * Time.deltaTime/2; //for jump
        movement = new Vector3(movement.x, playerY, movement.z);

        //moves character --------------------------------------------------------------------------
        charController.Move(movement);
    }
}
