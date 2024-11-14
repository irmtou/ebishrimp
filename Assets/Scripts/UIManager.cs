using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsView;
    [SerializeField] private GameObject optionsView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* Methods that are called by elements of the UI */
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

    public void ExitGame()
    {
        // exits the game (won't work in test mode)
        Application.Quit();
    }
}
