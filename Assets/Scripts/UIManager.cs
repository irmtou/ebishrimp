using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    /* The following vars are only applicable to the options scene */
    [Header("Canvases")]
    [SerializeField] private GameObject creditsView; // the empty game object associated with "credits screen"
    [SerializeField] private GameObject optionsView; // the empty game object associated with "options screen"
    [SerializeField] private GameObject titleView; // the empty game object associated with "title screen"
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sound;

    public void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "KitchenTIled")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleOptions();
            }
        }
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
    public void OpenTitle()
    {
        // loads the title scene
        SceneManager.LoadScene("SampleScene");
    }

    public void ToggleTitle()
    {
        // shows the credits if its not shown, or disables them otherwise
        if (titleView.activeSelf == true)
        {
            titleView.SetActive(false);
        } else 
        {
            titleView.SetActive(true);
        }
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
            if (creditsView.activeSelf == true)
            {
                creditsView.SetActive(false);
            }
            GameManager.Instance.Pause();
        }
    }

    public void PlayHoverSound() 
    {
        audioSource.PlayOneShot(sound); // Play button hover sound
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // This is for quitting in the editor
        #endif
    }
}
