using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateDifficulty : MonoBehaviour
{
    // gives symbolism to each of the difficulty buttons (in it's own script for simplicity of my brain)
    public int difficulty;
    public void changeDifficulty()
    {
        // applied to each UI button
        GameManager.Instance.difficulty = difficulty;
    }
}
