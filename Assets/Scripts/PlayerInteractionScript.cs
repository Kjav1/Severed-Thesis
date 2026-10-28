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

bool testBool = true;
bool otherTestBool = false;
List<string> BarkDialogue;
List<string> TestList, TestList2; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Guide on Characters, Items, and Bool Contexts
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

//Create More Characters by creating list, sorting and adding dialogue in Start using functions below

        TestList = new List<string>();
        TestList2 = new List<string>();
        TestList2.Add("Different Text!");
        TestList.Add("TestList Worked!");
        BarkDialogue = new List<string>();
        BarkDialogue.Add("Bark Bark! Test");

        
//How to add new Dialogue/Conditions: 
//DialogueTest1 represents customizable lists of dialogues  (List<string>)
//make a new list on the same level of DialogueTest1 and add it to V0/V1/V2 Dialogue List (if its dialogue, make it under bark if its bark)
//Amend a bool to the corresponding bool list (V0/V1/V2Bool)
//Furthest progress dialogue show up earlier, early progress shows up later (conditions check for first possible condition)

//-- 
//Sort Character List: 
//String Name, List, Bool List, Ante List, Perri, List, Post, List, 0-1-2 Barks Lists, 0-1-2 Dialogue Lists, 0-1-2 Bool Lists)
        
        SortCharacterLists("Veronica", Veronica, VBoolList, VAnte, VPerri, VPost, V0Barks, V1Barks, V2Barks, V0Dialogue, V1Dialogue, V2Dialogue, V0Bool, V1Bool, V2Bool);

        PeopleText = new Dictionary<string, string>();
        PeopleText.Add("Untagged", "ERROR: Text Not Found"); //for debugging tests!

        AddToList(TestList, "TestList1", "TestList2", "TestList3");

        AddDialogue("Veronica", 0, TestList, testBool, BarkDialogue);
        AddDialogue("Veronica", 1, TestList, testBool, BarkDialogue);
        AddDialogue("Veronica", 2, TestList, testBool, BarkDialogue);
        AddDialogue("Veronica", 0, TestList2, otherTestBool, BarkDialogue);

//order matters! Adding Dialogue will put whatever was added last to the front of the line! 
    }

    // Update is called once per frame
    void Update()
    {
        // string CheckingString = Veronica[state][0][0][0];
        //Debug.Log(CheckingString);
        state = PromManager.TimeState; //getting current state of world
        if (SearchForItemInList("item", ItemsHeld))
        {
            otherTestBool = true;
        }
    }

//Create a Character, has everything but individual context list strings (actual dialogue lists)
//String Name, List, Bool List, Ante List, Perri, List, Post, List, 0-1-2 Barks Lists, 0-1-2 Dialogue Lists, 0-1-2 Bool Lists)

    void SortCharacterLists(string Name, 
    List<List<List<List<string>>>> NameList, 
    List<List<bool>> NameBoolList, 
    List<List<List<string>>> NameAnte, 
    List<List<List<string>>> NamePerri, 
    List<List<List<string>>> NamePost, 
    List<List<string>> Name0Bark, 
    List<List<string>> Name1Bark, 
    List<List<string>> Name2Bark, 
    List<List<string>> Name0Dialogue, 
    List<List<string>> Name1Dialogue, 
    List<List<string>> Name2Dialogue,
    List<bool> Time0Bool,
    List<bool> Time1Bool,
    List<bool> Time2Bool)
    {
        //Ante, Perri, Post - Dialogue then Bark
        NameAnte.Add(Name0Dialogue);
        NameAnte.Add(Name0Bark);
        NamePerri.Add(Name1Dialogue);
        NamePerri.Add(Name1Bark);
        NamePost.Add(Name2Dialogue);
        NamePost.Add(Name2Bark);

        //Add Ante, Perri, Post to Character List
        NameList.Add(NameAnte);
        NameList.Add(NamePerri);
        NameList.Add(NamePost);

        //Add to Bool List
        NameBoolList.Add(Time0Bool);
        NameBoolList.Add(Time1Bool);
        NameBoolList.Add(Time2Bool);

        //Add to Character Bank
        Characters.Add(Name, NameList); 
        CharacterBools.Add(Name, NameBoolList);
    }

//Add 3 dialogue strings to list
    void AddToList(List<string> ListToAddTo, string Dialogue1, string Dialogue2, string Dialogue3)
    {
        ListToAddTo.Add(Dialogue1);
        ListToAddTo.Add(Dialogue2);
        ListToAddTo.Add(Dialogue3);
    }

//Add dialogue and Bark + Context to a character
    void AddDialogue(string characterName, int dialogueTimeState, List<string> DialogueToAdd, bool context, List<string> BarkDialogue)
    { //should add dialogue and bool at same place so never worry about not doing variable and dialogue right
        List<List<List<List<string>>>> CharacterToAdd = Characters[characterName];
        List<List<bool>> CharacterBoolToAdd = CharacterBools[characterName]; 

        CharacterBoolToAdd[dialogueTimeState].Insert(0, context);
        CharacterToAdd[dialogueTimeState][0].Insert(0, DialogueToAdd);
        CharacterToAdd[dialogueTimeState][1].Insert(0, BarkDialogue);
    } 

    public void Item(string tag)
    {
        ItemsHeld.Add(tag); //list starts at 0
    }

    bool SearchForItemInList(string Item, List<string> Inventory)
    {
        bool ItemFound = false;
        for (int i = 0; i<Inventory.Count; i++)
        {
            if (Inventory[i].ToLower() == Item.ToLower())
            {
                ItemFound = true;
            }
        } 
        return(ItemFound); 
    }
    
    public List<string> People(string tag, bool isBark) 
    {
        int counter = 0; 
        bool checkCondition = false; 
        int barkIndex;

//FUNCTION FOR FINDING WHAT LIST TO SEND
        while (checkCondition != true) 
        {
            if (CharacterBools[tag][state][counter]) { //finds earliest possible true condition, sends that counter back
                checkCondition = true;
            } else 
            {
                counter += 1;
            } //this isn't changing to 0
        }

        if (isBark)  //dialogue is first
        {
            barkIndex = 1; //bark quotes
        }
        else 
        {
            barkIndex = 0; //dialogue lines
        }

        List<string> Lines = Characters[tag][state][barkIndex][counter];
        return Lines; 
    }
}
