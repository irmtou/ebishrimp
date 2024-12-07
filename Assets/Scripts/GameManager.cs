using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // to access the game manager in other scripts simply reference it with:
    // GameManager.Instance.(whatever method you'd like to access)

    public static GameManager Instance { get; private set; }

    public int score{ get; private set;}

    public float time{ get; private set;}

    private bool ticking;

    public int shrimpCount{ get; private set;}

    void FixedUpdate()
    {
        //Check if health is below zero
        if(ticking)
        {
            time = time - Time.deltaTime;
        }
        if(time<=0f)
        {
            GameOver();
        }
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
        Application.targetFrameRate = 60; //We can change this
        LoadLevel("SampleScene");
    }

    private void NewGame()
    {
        score = 0;
        shrimpCount = 20;
        time = 120f;
        ticking  = true;
        LoadLevel("KitchenTlled");
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameOver()
    {
        NewGame();
        //SceneManager.LoadScene("Game Over");      //Uncomment if we make a Game Over screen
    }

    private void WinScreen()
    {
        NewGame();
        //SceneManager.LoadScene("Win Screen");      //Uncomment if we make a win screen 
    }

    //adds change value to shrimpCount, can be + or -
    public void changeShrimpCount(int change)
    {
        shrimpCount = shrimpCount + change;
        if (shrimpCount<=0)
        {
            //end game if so
            WinScreen();
        }
    }


}

