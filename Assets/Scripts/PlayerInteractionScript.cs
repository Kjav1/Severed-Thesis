using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] PromManagerScript PromManager; 

    int state; 

    bool testTrue = true;  //used for LIST TESTING

    List<string> ItemsHeld; 

    Dictionary <string, string> PeopleText;

    Dictionary <string, List<List<List<string>>>> Characters; 

    Dictionary <string, List<List<bool>>> CharacterBools;

//Veronica
    List<List<List<string>>> Veronica; 

    List<List<bool>> VBoolList; 

    List<List<string>> VAnte; 

    List<List<string>> VPost; 

    List<List<string>> VPerri; 

    //update the following to be list<list<string>>

    List<string> V0Barks, V0Dialogue, V1Barks, V1Dialogue, V2Barks, V2Dialogue; 

    List<bool> V0Bool, V1Bool, V2Bool; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemsHeld = new List<string>(); //create the list

        Characters = new Dictionary <string, List<List<List<string>>>>();
        CharacterBools = new Dictionary <string, List<List<bool>>>();

//Veronica ---
        Veronica = new List<List<List<string>>>(); //holds dialogue 
        VBoolList = new List<List<bool>>();
    //Holds Veronica's Information

        VAnte = new List<List<string>>(); 
        VPerri = new List<List<string>>(); 
        VPost = new List<List<string>>(); 

    //Dialogue Options - You make more Lists for each group

    //V0Dialogue = one of many possible lists to have in a dialogue option
        V0Barks = new List<string>(); 
        V0Dialogue = new List<string>(); 
        V0Bool = new List<bool>(); 

        V1Barks = new List<string>(); 
        V1Dialogue = new List<string>(); 
        V1Bool = new List<bool>(); 

        V2Barks = new List<string>(); 
        V2Dialogue = new List<string>(); 
        V2Bool = new List<bool>(); 


//Characters[Veronica][state][bark]
//stringList.InsertRange 

        V0Dialogue.Add("V0Dialogue");
        V0Dialogue.Add("V0DialogueTest");
        V1Dialogue.Add("V1Dialogue");
        V2Dialogue.Add("V2Dialogue");

        V0Barks.Add("V0Barks");
        V1Barks.Add("V1Barks");
        V2Barks.Add("V2Barks");

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

    public string People(string tag, bool isBark) 
    {
        int counter = 0; 
        bool checkCondition = false; 
        int barkIndex = 0;

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

        List<string> LineGroup = Characters[tag][state][barkIndex];
        //Characters[tag][state][barkIndex][counter];
        //Tag = Which Character
        //State = Ante, Peri, Post
        //BarkIndex, whether it's dialogue or bark 
        //Counter = which context it's in

        //Debug.Log(Characters[tag][state][barkIndex][counter]);
        Debug.Log(Characters[tag][0][0][0]);

        string SingleLine = Characters[tag][state][barkIndex][counter];
        //string Test = "test";
        //turn it into list that gets returned
        return SingleLine;
    }
}
