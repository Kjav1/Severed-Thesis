using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class CameraScript : MonoBehaviour
{
    InputAction lookAction; 
    Vector2 look; 
    float sensitivity = 120;
    float targetDistance = 14f; 
    Rigidbody rb; 

    [SerializeField] GameObject player; 
    [SerializeField] MovementScript movementScript; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        rb = this.GetComponent<Rigidbody>(); 
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
 
    void LateUpdate() //updates after character does movements, etc. helps make movements smooth
    {
        look = lookAction.ReadValue<Vector2>(); 
        float lookX = look.x; 

        transform.RotateAround(player.transform.position, Vector3.up, sensitivity*lookX*Time.deltaTime);        

       float distance = Vector3.Distance(this.transform.position, player.transform.position);

       if (distance > targetDistance)
        {
            transform.position = new Vector3(transform.position.x + movementScript.movement.x, 8f, transform.position.z + movementScript.movement.z);  //MoveToward left jittery lookAt
            //might have to check with gravity later too 

//            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);
//            transform.position = Vector3.Lerp(transform.position, Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), movementScript.speed * Time.deltaTime), 20* movementScript.speed * Time.deltaTime); //not fast enough in FixedUpdates
                // Vector3 newMovement = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 8f, player.transform.position.z), movementScript.speed * Time.deltaTime);                
                // transform.position = new Vector3(newMovement.x, 8f, newMovement.z);

            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 3.82f,player.transform.position.z), movementScript.speed * Time.deltaTime);
        } else if (distance < targetDistance-5) //push camera back if too close
        { 
//camera malfunctions if player gets too close, as player movement is based on camera's angle 
            Vector3 newLocation = new Vector3(transform.position.x+movementScript.movement.x, 8f, transform.position.z+movementScript.movement.z);
            transform.position = newLocation; //spent so long working on this, but found out pushing camera back, surprisingly, is same as pushing camera along with player... 
        }
        //transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, 8f, Time.deltaTime), transform.position.z);  
        transform.LookAt(player.transform.position);

    }
    
}
