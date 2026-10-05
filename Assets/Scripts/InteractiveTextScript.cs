using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class InteractiveScript : MonoBehaviour
{
    bool canInteract = false; 
    float distance; 
    [SerializeField] GameObject player; 
    [SerializeField] Camera cam; 
    [SerializeField] TimeCheckerScript timeScript; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject emptyObj; 
    public TMP_Text text;
    float currentAlpha = 0f; 
    InputAction interactAction;
    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");

        //setting up empty object to hold textMeshPro
        emptyObj = new GameObject("TEXT-" + this.gameObject.name); 

        emptyObj.transform.localScale = new Vector3(1,1,1); //keeps rotation from 'stretching'/squishing text
        
        emptyObj.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.localScale.y + 1, this.gameObject.transform.position.z);      

        //emptyObj.transform.parent = this.gameObject.transform; //parents new empty to current game object
        //removed parenting because it messed with scaling - causes stretch/squish when moving around

        emptyObj.AddComponent<TextMeshPro>(); //allows empty to hold text
        
        text = emptyObj.GetComponent<TextMeshPro>(); 
        text.SetText("Sample Text");
        
        //font size 10
        //alignment center
        text.fontSize = 7;
        text.alignment = TextAlignmentOptions.Center; //centered 

    }

    // Update is called once per frame
    void Update()
    {
        emptyObj.transform.LookAt(cam.transform, Vector3.up);
        emptyObj.transform.eulerAngles = new Vector3(0, 180 + emptyObj.transform.eulerAngles.y, 0); //180 + to flip it, otherwise looks opposite way
        
        text.color = new Color(255, 255, 255, currentAlpha); //opacity change

        distance = Vector3.Distance(this.transform.position, player.transform.position);
        //Debug.Log(distance);

        if (distance <= 7 && (timeScript == null || timeScript.isActive))
        {
            canInteract = true;
            currentAlpha = Mathf.Lerp(currentAlpha, 1, Time.deltaTime); //100% opacity = 1, 0% = 0

        } 
        else if (timeScript == null || timeScript.isActive) //order matters - check null first, otherwise get error when unable to check variable
        {
            currentAlpha = Mathf.Lerp(currentAlpha, 0, Time.deltaTime * 10); //graceful exit 
            canInteract = false; 

        } else
        {
            currentAlpha = 0; //disappears entirely, when time is changed
        }

        if (interactAction.WasPressedThisFrame()) //needs to be in Update otherwise it won't react every time button is pressed
        {
            if (canInteract)
            {
                if (this.tag == "item") //for items to pick up when interacted with
                {
                    //change this into: 
                    //ItemsScript.interact()
                    //the other side will check its tag, then destroy or set some manager script active depending on what it is
                    Destroy(this.gameObject);
                    Destroy(emptyObj);
                }
                currentAlpha = 0;
                text.SetText("Hello!"); 
                //Debug.Log("Interact!!"); //in Fixed Update, only runs every now and then
            }
            
        }

    }
}
