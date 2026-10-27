using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] PromManagerScript PromManager; 

    int state; 

    bool testTrue = true;  //used for LIST TESTING

    List<string> ItemsHeld; 

    Dictionary <string, string> PeopleText;

    Dictionary <string, List<List<List<List<string>>>>> Characters; 

    Dictionary <string, List<List<bool>>> CharacterBools;

//Veronica
    List<List<List<List<string>>>> Veronica; 

    List<List<bool>> VBoolList; 

    List<List<List<string>>> VAnte, VPost, VPerri; 

    //update the following to be list<list<string>>

    List<List<string>> V0Barks, V0Dialogue, V1Barks, V1Dialogue, V2Barks, V2Dialogue; 

    List<string> DialogueTest1, BarkTest1; 

    List<bool> V0Bool, V1Bool, V2Bool; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemsHeld = new List<string>(); //create the list

        Characters = new Dictionary <string, List<List<List<List<string>>>>>();
        CharacterBools = new Dictionary <string, List<List<bool>>>();

// //Veronica ---
         Veronica = new List<List<List<List<string>>>>(); //holds dialogue 
         VBoolList = new List<List<bool>>();
//     //Holds Veronica's Information

         VAnte = new List<List<List<string>>>(); 
         VPerri = new List<List<List<string>>>(); 
         VPost = new List<List<List<string>>>(); 

//     //Dialogue Options - You make more Lists for each group

//     //V0Dialogue = one of many possible lists to have in a dialogue option
        V0Barks = new List<List<string>>(); 
        V0Dialogue = new List<List<string>>(); 
        V0Bool = new List<bool>(); 

        V1Barks = new List<List<string>>(); 
        V1Dialogue = new List<List<string>>(); 
        V1Bool = new List<bool>(); 

        V2Barks = new List<List<string>>(); 
        V2Dialogue = new List<List<string>>(); 
        V2Bool = new List<bool>(); 

        DialogueTest1 = new List<string>();
        BarkTest1 = new List<string>();

        DialogueTest1.Add("Test!");
        DialogueTest1.Add("Second Test!");
        DialogueTest1.Add("Third Test!");
        BarkTest1.Add("Bark!");

        V0Dialogue.Add(DialogueTest1);
        V1Dialogue.Add(DialogueTest1);
        V2Dialogue.Add(DialogueTest1);

        V0Barks.Add(BarkTest1);
        V1Barks.Add(BarkTest1);
        V2Barks.Add(BarkTest1);

        VAnte.Add(V0Dialogue); 
        VAnte.Add(V0Barks);

        VPerri.Add(V1Dialogue); 
        VPerri.Add(V1Barks);
         
        VPost.Add(V2Dialogue); 
        VPost.Add(V2Barks);

        Veronica.Add(VAnte);
        Veronica.Add(VPerri);
        Veronica.Add(VPost);

        VBoolList.Add(V0Bool);
        VBoolList.Add(V1Bool);
        VBoolList.Add(V2Bool);
         
        V0Bool.Add(testTrue);
        V1Bool.Add(testTrue);
        V2Bool.Add(testTrue);

//How to add new Dialogue/Conditions: 
//DialogueTest1 represents customizable lists of dialogues  (List<string>)
//make a new list on the same level of DialogueTest1 and add it to V0/V1/V2 Dialogue List (if its dialogue, make it under bark if its bark)
//Amend a bool to the corresponding bool list (V0/V1/V2Bool)
//Furthest progress dialogue show up earlier, early progress shows up later (conditions check for first possible condition)

//-- 

        PeopleText = new Dictionary<string, string>();
        PeopleText.Add("Untagged", "ERROR: Text Not Found"); //for debugging tests!
        PeopleText.Add("test", "testing!!!"); //for testing its validity

        Characters.Add("Veronica", Veronica);
        CharacterBools.Add("Veronica", VBoolList);
    }

    // Update is called once per frame
    void Update()
    {
        state = PromManager.TimeState; //getting current state of world
    }

    public void Item(string tag)
    {
        ItemsHeld.Add(tag); //list starts at 0
    }

    
    
    public List<string> People(string tag, bool isBark) 
    {
        int counter = 0; 
        bool checkCondition = false; 
        int barkIndex = 0;



//FUNCTION FOR FINDING WHAT LIST TO SEND
        while (checkCondition != true) 
        {
            if (CharacterBools[tag][state][counter]) { //finds earliest possible true condition, sends that counter back
                checkCondition = true;
            } else 
            {
                counter += 1;
            }
        }

        if (isBark)  //dialogue is first
        {
            barkIndex = 1;
        }
        else 
        {
            barkIndex = 0;
        }

        //List<string> LineGroup = Characters[tag][state][barkIndex];

        List<string> Lines = Characters[tag][state][barkIndex][counter];
        return Lines; 
    }
}
