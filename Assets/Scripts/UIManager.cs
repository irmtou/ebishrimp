using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    /* The following vars are only applicable to the options scene */
    [SerializeField] private GameObject creditsView; // the empty game object associated with "credits screen"
    [SerializeField] private GameObject optionsView; // the empty game object associated with "options screen"
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* Methods that are called by elements of the UI */
    public void StartGame()
    {
        // loads the options scene
        SceneManager.LoadScene("SampleTiledKitchen");
    }
    public void OpenOptions()
    {
        // loads the options scene
        SceneManager.LoadScene("Options");
    }

    public void OpenTitle()
    {
        // loads the title scene
        SceneManager.LoadScene("SampleScene");
    }

    public void ToggleCredits()
    {
        // shows the credits if its not shown, or disables them otherwise
        if (creditsView.activeSelf == true)
        {
            creditsView.SetActive(false);
            optionsView.SetActive(true); 
        } else 
        {
            creditsView.SetActive(true);
            optionsView.SetActive(false); // disable options so the buttons aren't accidentally triggered
        }
    }
    public void ToggleOptions()
    {
        //TODO: PAUSE THE GAME IN THE BACKGROUND
        //^^ This section is dangerous because it doesn't stop anything going on in the background while the game is "paused"
        // shows the credits if its not shown, or disables them otherwise
        if (optionsView.activeSelf == true)
        {
            optionsView.SetActive(false);
        } else 
        {
            optionsView.SetActive(true);
        }
    }

    public void ExitGame()
    {
        // exits the game (won't work in test mode)
        Application.Quit();
    }
}
