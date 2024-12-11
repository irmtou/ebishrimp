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
        GameManager.Instance.NewGame();
    }
    public void RestartGame()
    {
        GameManager.Instance.ResetGame();
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
        if (optionsView.activeSelf == true)
        {
            optionsView.SetActive(false);
            GameManager.Instance.Unpause();
        } else 
        {
            optionsView.SetActive(true);
            GameManager.Instance.Pause();
        }
    }

    public void ExitGame()
    {
        // exits the game (won't work in test mode)
        Application.Quit();
    }
}
