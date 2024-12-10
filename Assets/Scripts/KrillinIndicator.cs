using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrillinIndicator : MonoBehaviour
{
    [SerializeField] private GameObject promptMessage; // the press e to interact message that pops up\
    
    public AirFryerInteraction airFryer;
    public DryerInteraction dryerCenter;
    public DryerInteraction dryerCounter;
    public WasherInteraction washerCenter;
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
       promptInteraction();
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
                promptMessage.gameObject.SetActive(true);
            } 
            else 
            {
                promptMessage.gameObject.SetActive(false);
            }
    }



}
