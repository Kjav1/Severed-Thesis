using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    Light LightSwitch;
    Color PreLight;
    Color PeriLight;
    Color PostLight;
    float lightTemp = 0; 
    List<Color> ColorList;
    List<float> IntensityList;
    List<float> TempList; 
    bool tempChange = false; 
    float PromLightTemp = 8000f;
    int State = 0;
    float DeltaLightChange = 5f;
    [SerializeField] PromManagerScript PromScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LightSwitch = this.GetComponent<Light>();
        PreLight = new Color(9f, 7f, 4f);
        PeriLight = new Color(6f, 2f, 5f);
        PostLight = new Color(2f, 2f, 5f);

        ColorList = new List<Color>{PreLight, PeriLight, PostLight};
        IntensityList = new List<float>{0.02f, 0.08f, 0.02f};
        TempList = new List<float>{3774f, 8000f, 20000f};
    }

    // Update is called once per frame
    void Update()
    {
        State = PromScript.TimeState; 

        LightSwitch.color = ColorList[State];
        LightSwitch.intensity = IntensityList[State];


        if (tempChange)
        {
            ChangeTemp(DeltaLightChange);
            if (PromLightTemp >= 10000f)
            {
                tempChange = false;
            }
        } else 
        {
            ChangeTemp(-DeltaLightChange);
        }

        if (State == 1)
        {
            LightSwitch.colorTemperature = PromLightTemp;
        } else {
            LightSwitch.colorTemperature = TempList[State];
        }
    }

    void ChangeTemp(float number)
    {
        PromLightTemp += number;
        if (PromLightTemp <= 5010f)
        {
            tempChange = true;
        }
    }


}
