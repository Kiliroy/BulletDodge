using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {
    /// <summary>
    /// Script used for loading scenes and ending the game
    /// </summary>
    /// <param name="Level"></param>

    public void LoadScene(int Level)
    {
        if (Level == -1)
            Application.Quit();
        else
            SceneManager.LoadScene(Level);
    }
}
