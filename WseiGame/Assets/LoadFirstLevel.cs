using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFirstLevel : MonoBehaviour
{
    public UnityEngine.UI.Toggle toggleButton;
    bool tutorial = true;


    public void LoadNextLevel()
    {
        tutorial = toggleButton.isOn;
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 0 && tutorial == false)
            currentLevel++;
        if (currentLevel < 4)
            SceneManager.LoadScene(++currentLevel);
    }
}
