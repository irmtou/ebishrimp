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
    public StovetopInteraction stoveTop1;
    public StovetopInteraction stoveTop2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (airFryer.isPlayerNear 
        || dryerCenter.isPlayerNear
        || dryerCounter.isPlayerNear
        || washerCenter.isPlayerNear
        || stoveTop1.isPlayerNear
        || stoveTop2.isPlayerNear
            )
            {
                promptMessage.gameObject.SetActive(true);
            } 
            else 
            {
                promptMessage.gameObject.SetActive(false);
            }
    }

    public void promptInteraction()
    {
        if (promptMessage) // will do nothing if there is no prompt message in the scene
        {
            
            promptMessage.gameObject.SetActive(true);
        }
    }



}
