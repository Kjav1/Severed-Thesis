using System;
using UnityEngine;

public class TimeCheckerScript : MonoBehaviour
{
    [SerializeField] PromManagerScript PromManagerScript;  
    public int stateID; //which state this object belongs in (1, 2, or 3)
    int state; //current state, determined by Manager script
    Collider collider; 
    Rigidbody rb; 
    MeshRenderer mesh; 
    public bool isActive; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = this.gameObject.GetComponent<Collider>();
        mesh = this.gameObject.GetComponent<MeshRenderer>();
        rb = this.gameObject.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        state = PromManagerScript.TimeState; 
        if (state == stateID)
        {
            //this.gameObject.SetActive(true); //can't bring objects back if they're not active
            collider.isTrigger = false; 
            isActive = true; //for interactables
            mesh.enabled = true; 
            if (rb != null)
            {
                rb.isKinematic = false;  //stops any physics, keeps things from phasing through the floor (hopefully)
            }
        } else //reverses everything
        {
            //this.gameObject.SetActive(false);
            collider.isTrigger = true; 
            mesh.enabled = false; 
            isActive = false; //for interactables
            if (rb != null)
            {
                rb.isKinematic = true;  
            }
        }
    }

//If next state (state + 1) equals current StateID, then blocked is treu
//If current state is 2 and stateID is 0, then blocked is true
    void OnTriggerEnter(Collider other) //so player can't spawn into objects
    {
        if (other.gameObject.tag == "player") //how to get previous state
        {
                PromManagerScript.blocked = true;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        PromManagerScript.blocked = false;       
    }
}
