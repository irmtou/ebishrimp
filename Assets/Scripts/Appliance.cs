using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AnimationModule;

public class Appliance : MonoBehaviour
{
    public bool poweredOn = false; // appliances are defaulted to off
    [SerializeField] private Animation runningApplianceAnim; // when the appliance is running
    [SerializeField] private Animation openingAnim; // when the door opens
    [SerializeField] private Animation closingAnim; // when the door closes
    [SerializeField] private Animation applianceAnim; // Animation controller
    [SerializeField] private string runningApplianceName; // when the appliance is running
    [SerializeField] private string openingName; // when the door opens
    [SerializeField] private string closingName; // when the door closes

    void Update()
    {
        
        if (poweredOn)
        {
            if (applianceAnim.isPlaying || runningApplianceName == null)
            {
                return;
            }
            running();
        }


    }


    void running()
    {
        // Called when the appliance is powered on 
        // if runningApplianceAnimation != null
        // set any "running appliance animations"
        try
        {
            applianceAnim.Play(runningApplianceName);
        }
        catch
        {
            Debug.Log("Could not play the running appliance animation");
        }
    }


    void openAppliance()
    {
        // If an appliance has a door, this is triggered when it is opening
        //TODO: Figure out how to call animations from functions
        applianceAnim.Play(openingName);
    }

    void closeAppliance()
    {
        // If an appliance has a door, this is triggered when it is closing
        applianceAnim.Play(closingName);
    }

    
}
