using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] PromManagerScript PromManager; 

    int state; 

    bool testTrue = true;  //used for LIST TESTING

    List<string> ItemsHeld; 

    Dictionary <string, string> PeopleText;

//Veronica
    List<List<List<string>>> Veronica; 

    List<List<bool>> VBoolList; 

    List<List<string>> VAnte; 

    List<List<string>> VPost; 

    List<List<string>> VPerri; 

    List<string> V0Barks, V0Dialogue, V1Barks, V1Dialogue, V2Barks, V2Dialogue; 

    List<bool> V0Bool, V1Bool, V2Bool; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemsHeld = new List<string>(); //create the list


//Veronica ---
        Veronica = new List<List<List<string>>>(); //holds dialogue 
        VBoolList = new List<List<bool>>();

        VAnte = new List<List<string>>(); 
        VPerri = new List<List<string>>(); 
        VPost = new List<List<string>>(); 

        V0Barks = new List<string>(); 
        V0Dialogue = new List<string>(); 
        V0Bool = new List<bool>(); 

        V1Barks = new List<string>(); 
        V1Dialogue = new List<string>(); 
        V1Bool = new List<bool>(); 

        V2Barks = new List<string>(); 
        V2Dialogue = new List<string>(); 
        V2Bool = new List<bool>(); 

//stringList.InsertRange 

        V0Dialogue.Add("V0Dialogue");
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
        
        //Dialogue bits below: 

        
//-- 

        PeopleText = new Dictionary<string, string>();
        PeopleText.Add("Untagged", "ERROR: Text Not Found"); //for debugging tests!
        PeopleText.Add("test", "testing!!!"); //for testing its validity
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
            if (VBoolList[state][counter]) {
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

        string Test = Veronica[state][barkIndex][counter];
        //string Test = "test";
        return Test;
    }
}
