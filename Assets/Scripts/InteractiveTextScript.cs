using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class InteractiveScript : MonoBehaviour
{
    bool canInteract = false; 
    bool isBark = true;
    string responseText;
    float distance; 
    List<string> Lines;
    int indexCounter = -1;
    [SerializeField] GameObject player; 
    [SerializeField] Camera cam; 
    [SerializeField] TimeCheckerScript timeScript; 
    [SerializeField] PlayerInteractionScript interactScript; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject emptyObj; 
    public TMP_Text text;
    float currentAlpha = 0f; 
    InputAction interactAction;
    void Start()
    {
        isBark = true; 

        interactAction = InputSystem.actions.FindAction("Interact");

        //setting up empty object to hold textMeshPro
        emptyObj = new GameObject("TEXT-" + this.gameObject.name); 

        emptyObj.transform.localScale = new Vector3(1,1,1); //keeps rotation from 'stretching'/squishing text
        
        emptyObj.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.localScale.y + 1, this.gameObject.transform.position.z);      

        //emptyObj.transform.parent = this.gameObject.transform; //parents new empty to current game object
        //removed parenting because it messed with scaling - causes stretch/squish when moving around

        emptyObj.AddComponent<TextMeshPro>(); //allows empty to hold text
        
        text = emptyObj.GetComponent<TextMeshPro>(); 
        
        //font size 10
        //alignment center
        text.fontSize = 7;
        text.alignment = TextAlignmentOptions.Center; //centered 
    }

    // Update is called once per frame
    void Update()
    {

        //Setting up Text and Resetting Barks 
        if (Lines == null) { //has to be set up here, won't run in start - this keeps it running once at beginning, even in update
            if (this.gameObject.layer != 6) 
            {
                Lines = interactScript.People(this.tag, isBark);
                responseText = Lines[0];
            } else 
            {
                responseText = this.tag;
            }
            text.SetText(responseText);
        } else if (this.gameObject.layer != 6 && isBark) {
            Lines = interactScript.People(this.tag, isBark);
            text.SetText(Lines[0]);

        }

        //vector3 transform but z is this.transform - keeps a forward appearance, looking at player
        emptyObj.transform.LookAt(cam.transform, Vector3.up);
        emptyObj.transform.eulerAngles = new Vector3(0, 180 + emptyObj.transform.eulerAngles.y, 0); //180 + to flip it, otherwise looks opposite way
        
        text.color = new Color(255, 255, 255, currentAlpha); //opacity change

        distance = Vector3.Distance(this.transform.position, player.transform.position);

        //Can Interact, Words Fade In
        if (distance <= 7 && (timeScript == null || timeScript.isActive))
        {
            canInteract = true;
            currentAlpha = Mathf.Lerp(currentAlpha, 1, Time.deltaTime); //100% opacity = 1, 0% = 0

        } 
        else if (timeScript == null || timeScript.isActive) //order matters - check null first, otherwise get error when unable to check variable
        {
            currentAlpha = Mathf.Lerp(currentAlpha, 0, Time.deltaTime * 10); //graceful exit 
            canInteract = false; 
            if (currentAlpha <= 0.08f) 
            {
                isBark = true;
                indexCounter = -1;
            }

        } else
        {
            currentAlpha = 0; //disappears entirely, when time is changed
        }

        if (interactAction.WasPressedThisFrame()) //needs to be in Update otherwise it won't react every time button is pressed
        {
            if (canInteract)
            {
                isBark = false;
                //possibly for safety, do a thing to find layer of items and then use that variable 
                if (this.gameObject.layer == 6) //for items to pick up when interacted with
                {
                    interactScript.Item(this.tag);
                    Destroy(this.gameObject);
                    Destroy(emptyObj);

                } else {
                    Lines = interactScript.People(this.tag, isBark);
                    indexCounter += 1; 
                    //if lines greater length, reset to 0
                    if (indexCounter > Lines.Count-1)
                    {
                        indexCounter = 0;
                    }

                    text.SetText(Lines[indexCounter]); //goes through chats
                }
                currentAlpha = 0;
            }
            
        }

    }
}
