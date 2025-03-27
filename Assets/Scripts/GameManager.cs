using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // to access the game manager in other scripts simply reference it with:
    // GameManager.Instance.(whatever method you'd like to access)

    public static GameManager Instance { get; private set; }
    public int cookedShrimpCount{ get; private set;}
    public float time;
    public bool ticking;
    public int shrimpCount{ get; private set;}
    public int difficulty; // an int that controls the settings of everything to adjust difficulty
    public int score; // holds the current score of the player
    public int highScore = 0; // variable to store the high score
    private const string HIGH_SCORE_KEY = "highScore"; // Key for PlayerPrefs
   

    void FixedUpdate()
    {
        if(ticking)
        {
            score = (int) (cookedShrimpCount*time);
            time = time - Time.deltaTime;
            if(time<=0f)
            {
                UpdateHighScore(score); // update hs if you lose 
                GameOver();
                ticking = false;
                
            }
        }

    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }


    //Called when script is being loaded
    private void Awake()
    {
        //If instance of GameManager exists destroy the additional one
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        //Else this is the new instance
        else{
            Instance = this;
            highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        difficulty = 2;
        ResetGame(); // Moved all of these functions to reset game so we would have a callable reset function
    }

    public void ResetGame()
    {

        Application.targetFrameRate = 60; //We can change this
        ticking  = false;
        shrimpCount = 1;
        time = 120f;
        LoadLevel("SampleScene");
    }

    public void NewGame()
    {
        score = 0;
        cookedShrimpCount = 0;
        ticking  = true;
        LoadLevel("KitchenTIled");
        Time.timeScale = 1;
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameOver()
    {
        
        SceneManager.LoadScene("Crustacean Devastation");      // Game Over Screen (Loss)
    }

    private void WinScreen()
    {
        
        SceneManager.LoadScene("KrilledIt");     // Game Over Screen (Win)
    }

    // sets shrimp count
    public void changeShrimpCount(int change)
    {
        shrimpCount = change;
        if (shrimpCount<=0)
        {
            //end game if so
            UpdateHighScore(score); // update hs if you win
            ticking = false;
            new WaitForSeconds(2f);
            WinScreen();
        }
    }
    public void changeCookedShrimpCount(int change)
    {
        cookedShrimpCount = change;
    }

    // Call this method when the player's score is updated
    public void UpdateHighScore(int currentScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            // Save the new high score to PlayerPrefs
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save(); // Ensure immediate saving
        }
    }


}

