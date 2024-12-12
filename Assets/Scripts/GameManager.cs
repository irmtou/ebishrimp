using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // to access the game manager in other scripts simply reference it with:
    // GameManager.Instance.(whatever method you'd like to access)

    public static GameManager Instance { get; private set; }

    public int cookedShrimpCount{ get; private set;}

    public float time{ get; private set;}

    public bool ticking;

    public int shrimpCount{ get; private set;}
   

    void FixedUpdate()
    {
        if(ticking)
        {
            
            time = time - Time.deltaTime;
            if(time<=0f)
            {
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
       ResetGame(); //Moved all of these functions to reset game so we would have a callable reset function
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

    //sets shrimp count
    public void changeShrimpCount(int change)
    {
        shrimpCount = change;
        if (shrimpCount<=0)
        {
            //end game if so
            ticking = false;
            new WaitForSeconds(2f);
            WinScreen();
        }
    }
    public void changeCookedShrimpCount(int change)
    {
        cookedShrimpCount = change;
    }


}

