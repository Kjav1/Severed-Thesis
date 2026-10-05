using UnityEngine;
using UnityEngine.InputSystem;

public class PromManagerScript : MonoBehaviour
{
    public int TimeState = 0; //Time Zone Change
    public bool Glitch1 = false; //glitch 1 ability: 
    public bool Glitch2 = false; //glitch 2 ability: 
    public bool blocked = false; 
    InputAction glitchAction; 
    [SerializeField] GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        glitchAction = InputSystem.actions.FindAction("GlitchEffect"); 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //fixed or late update won't work with press button event
    {
        if (glitchAction.WasPressedThisFrame() && !blocked)
        {
            //Change this Later: currently just fliips a switch; later we'll figure out which input and how to better switch them out
            if (TimeState >=2) {TimeState = 0;} else {TimeState ++;}; 

            //TIME STATES: 
            //0 -- Pre-Prom -- Setup
            //1 -- During Prom -- Prom Queen
            //2 -- Post Prom - Slasher

            if (Glitch1) {Glitch1 = false;} else {Glitch1 = true;} //for now also flips a switch here

            if (Glitch2) {Glitch2 = false;} else {Glitch2 = true;}

            //Debug.Log(TimeState + "|" + Glitch1 + "|" + Glitch2);

        }
    }
}
