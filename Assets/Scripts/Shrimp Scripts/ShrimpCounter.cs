using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;

public class ShrimpCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI shrimpCounter;
    public TextMeshProUGUI time;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance != null)
        {
            shrimpCounter.text = "Shrimp Counter: " + GameManager.Instance.shrimpCount.ToString("00");
            int timeNum = (int) GameManager.Instance.time;
            time.text = timeNum.ToString("000");
        }
    }
}

