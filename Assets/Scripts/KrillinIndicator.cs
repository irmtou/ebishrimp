using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrillinIndicator : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject promptMessage; // the press e to interact message that pops up
    [SerializeField] private GameObject busyMessage; // tells the user that the appliance is busy
    [Header("Connected Managers")]
    [SerializeField] private CookingAppliance chef;
    
    [Header("Connected Appliances")]
    public ApplianceInteraction airFryer;
    public ApplianceInteraction dryerCenter;
    public ApplianceInteraction dryerCounter;
    public ApplianceInteraction washerCenter;
    public ApplianceInteraction stoveTop1;
    public ApplianceInteraction stoveTop2;
    public ApplianceInteraction cutBoard1;
    public ApplianceInteraction cutBoard2;
    public ApplianceInteraction mixer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       promptInteractionv2();
    }

    public void promptInteraction()
    {
        if (airFryer.isPlayerNear 
        || dryerCenter.isPlayerNear
        || dryerCounter.isPlayerNear
        || washerCenter.isPlayerNear
        || stoveTop1.isPlayerNear
        || stoveTop2.isPlayerNear
        || cutBoard1.isPlayerNear
        || cutBoard2.isPlayerNear
        || mixer.isPlayerNear
            )
            {
                if (chef.isCooking)
                {
                    busyMessage.gameObject.SetActive(true);
                    promptMessage.gameObject.SetActive(false);
                } else
                {
                    promptMessage.gameObject.SetActive(true);
                    busyMessage.gameObject.SetActive(false);
                }
                
            } 
            else 
            {
                busyMessage.gameObject.SetActive(false);
                promptMessage.gameObject.SetActive(false);
            }
            
    }
    private void promptInteractionv2()
    {
        if (airFryer.isPlayerNear 
        || dryerCenter.isPlayerNear
        || dryerCounter.isPlayerNear
        || washerCenter.isPlayerNear
        || stoveTop1.isPlayerNear
        || stoveTop2.isPlayerNear
        || cutBoard1.isPlayerNear
        || cutBoard2.isPlayerNear
        || mixer.isPlayerNear
            )
            {
                if (airFryer.currentlyCooking
                    || dryerCenter.currentlyCooking
                    || dryerCounter.currentlyCooking
                    || washerCenter.currentlyCooking
                    || stoveTop1.currentlyCooking
                    || stoveTop2.currentlyCooking
                    || cutBoard1.currentlyCooking
                    || cutBoard2.currentlyCooking
                    || mixer.currentlyCooking
                        )
                {
                    busyMessage.gameObject.SetActive(true);
                    promptMessage.gameObject.SetActive(false);
                } else
                {
                    promptMessage.gameObject.SetActive(true);
                    busyMessage.gameObject.SetActive(false);
                }
                
            } 
            else 
            {
                busyMessage.gameObject.SetActive(false);
                promptMessage.gameObject.SetActive(false);
            }
    }



}
