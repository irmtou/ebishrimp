using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;


// updates the UI for the score, shrimp counter, and the time remaining
public class ShrimpCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI shrimpCounter;
    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;

    // Update is called once per frame
    void Update()
    {
        // each UGUI component is optional and updated accordingly (less bugs, more maleable)
        if(GameManager.Instance != null)
        {
            if (shrimpCounter != null)
            {
                shrimpCounter.text = "Shrimp Counter: " + GameManager.Instance.shrimpCount.ToString("00");
            }
            if (time != null)
            {
                int timeNum = (int) GameManager.Instance.time;
                time.text = timeNum.ToString("000");
            }
            if (score != null)
            {
                score.text = "Score: " + GameManager.Instance.score.ToString("000");
            }
            if (highScore != null)
            {
                highScore.text = "High Score: " + GameManager.Instance.highScore.ToString("000");
            }
            
        }
    }
}

