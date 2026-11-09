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
            mesh.enabled = true; //visible


            transform.LookAt(player.transform); //look at player

            speed = Mathf.Lerp(speed, 0, Time.deltaTime); //used to slow down movement for lurch appearance
            
            if (speed <= 0.3) //recharging after lurch
            {
                speed = Random.Range(8, 15 + SpeedOverTime);
            }

            //movement
            rb.linearVelocity = transform.forward * speed;

            distance = Vector3.Distance(this.transform.position, player.transform.position);

            if (distance > maxDistance)
            {
                //if too far, will pop up near player
                transform.position = player.transform.position - player.transform.forward; 
            }
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