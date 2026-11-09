using System.Diagnostics.Metrics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopmanagerScript : MonoBehaviour
{

bool loopStart = false; 
float counter = 0; 
public bool IsActive = false;

void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("keep"); //creating a singleton

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene("SampleScene");
        loopStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (loopStart == true)
        {
            counter += Time.deltaTime; 
            //Debug.Log(counter); 
            if (counter >= 15) //change timer to better duration
            {
                // Loop();
                ActivateGlitch();
                // SceneManager.LoadScene("SampleScene");
                // counter = 0;
            }
        }
    }
    
    void ActivateGlitch() 
    {
        IsActive = true;
    }

    public void Loop()
    {
        SceneManager.LoadScene("SampleScene");
        counter = 0;
        IsActive = false;
    }
}
