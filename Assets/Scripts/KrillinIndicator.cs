using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  UI: Text that pops up to show the player that they can interact with things, 
    may add more than just appliances here  */

public class KrillinIndicator : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject promptMessage; // the press e to interact message that pops up
    [SerializeField] private GameObject busyMessage; // tells the user that the appliance is busy
    [Header("Connected Managers")]
    [SerializeField] private CookingAppliance chef;
    
    [Header("Connected Appliances")]
    public ApplianceInteraction[] connectedAppliances;

    // Update is called once per frame
    void Update()
    {
       promptInteraction(); // calls the promptInteraction button, a UI script really
    }

    private void promptInteraction()
    {
        ApplianceInteraction nearbyAppliance = null; // makes a temporary nearbyAppliance
        foreach (var appliance in connectedAppliances) {
            if (appliance.isPlayerNear) {
                nearbyAppliance = appliance;
                break; // Exits the loop once one is found
            }
        }

        if (nearbyAppliance != null) {
            // Call the specific appliance to see if it's cooking
            if (nearbyAppliance.currentlyCooking)
                {
                    // busy appliance
                    busyMessage.gameObject.SetActive(true);
                    promptMessage.gameObject.SetActive(false);
                } else
                {
                    // can press F
                    promptMessage.gameObject.SetActive(true);
                    busyMessage.gameObject.SetActive(false);
                }
        }
        else
        {
            // not near an appliance
            busyMessage.gameObject.SetActive(false);
            promptMessage.gameObject.SetActive(false);
        }

    }


}
