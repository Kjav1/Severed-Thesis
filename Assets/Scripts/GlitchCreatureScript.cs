using UnityEngine;

public class GlitchCreatureScript : MonoBehaviour
{
    Rigidbody rb; 
    Vector3 Target; 
    float SpeedOverTime; 
    float speed = 5;
    float distance; 
    float maxDistance = 25f;
    public bool Active; 
    bool firstActive = true;
    bool MoveTarget = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject player;

    GameObject manager; 

    LoopmanagerScript LoopScript;

    MeshRenderer mesh;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.FindGameObjectWithTag("keep");
        LoopScript = manager.GetComponent<LoopmanagerScript>();
        mesh = this.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Active = LoopScript.IsActive; 

        if (!Active) {
            mesh.enabled = false; //invisible, just in case
        } else 
        {
            distance = Vector3.Distance(this.transform.position, player.transform.position);
            
            mesh.enabled = true; //visible
            if (firstActive)
            {
                if (distance > maxDistance)
                {
                    //if too far, will pop up near player
                    transform.position = player.transform.position - new Vector3(Random.Range(-10, 10), player.transform.position.y, Random.Range(-10, 10)); 
                }
                firstActive = false;
            }
            // if (player.transform.position.y >= 1.1f)
            // {
            //     if (MoveTarget)
            //     {
            //         this.transform.position = new Vector3(23.97f, 5.81f, 25.2f);
            //         MoveTarget = false;
            //     }
            // } else 
            // {
            //     MoveTarget = true;
            // }

            //so that the enemy doesn't look/bend in ways it shouldn't 
            Vector3 LookAtPlayer = player.transform.position;
            LookAtPlayer.y = this.transform.position.y; 
            transform.LookAt(LookAtPlayer); //look at player

            speed = Mathf.Lerp(speed, 0, Time.deltaTime); //used to slow down movement for lurch appearance
            
            if (speed <= 0.3) //recharging after lurch
            {
                speed = Random.Range(8, 15 + SpeedOverTime);
            }

            if (distance > maxDistance)
                {
                    //Debug.Log(player.transform.position.y);
                    //if too far, will pop up near player

                    //check if you can spawn there? - instantiate a collision block, if blocked, then don't

                    //work out how to make sure creature doesn't go out of bounds (stairs)
                    //teleports under second floor
                    //transform.position = player.transform.position - new Vector3(Random.Range(-10, 10), player.transform.position.y-2f, Random.Range(-10, 10)); 
                }

            //movement
            Vector3 movement = transform.forward * speed;
            movement.y = rb.linearVelocity.y; //keep gravity
            rb.linearVelocity = movement;
            }
    }

    void OnCollisionEnter(Collision otherObject) 
    {
        if (otherObject.gameObject.layer == 7)
        {
            if (LoopScript != null)
            {
                LoopScript.Loop(); //loop stage
            }
        }
    }
}