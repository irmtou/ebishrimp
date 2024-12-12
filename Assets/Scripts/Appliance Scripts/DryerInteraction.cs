using UnityEngine;

public class DryerInteraction : MonoBehaviour
{
    public Animator animator;
    private bool isPlayerNear = false;
    private bool isDoorOpen = false;
    private bool isPlayerInside = false;
    // private bool isDoorClosedWithPlayerInside = false; // not used
    [SerializeField] private GameObject promptMessage;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isDoorOpen)
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        animator.SetTrigger("OpenDoor");
        isDoorOpen = true;
        Debug.Log("Dryer door opened.");
        animator.SetTrigger("CloseDoor");
        isDoorOpen = false;
        Debug.Log("Dryer door opened.");
    }


    private void EnterDryer()
    {
        if (!isPlayerInside)
        {
            isPlayerInside = true;
            Debug.Log("Player entered the dryer.");
        }
    }

    private void StartDryer()
    {
        animator.SetTrigger("StartDryer");
        Debug.Log("Dryer started.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // when the player enters the interaction zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player is near an appliance.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // when the player leaves the interaction zone
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left the appliance.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // when the player is trying to access a running appliance
        if (other.CompareTag("Player")) // Example key for entering the air fryer
        {
            // maybe add some logic encouraging the player to wait
            Debug.Log("Wait for the appliance to finish cooking");
        }
    }

}