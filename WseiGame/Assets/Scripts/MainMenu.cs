using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    bool isTutorialOn = true;

    public void StartNewGame()
    {
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void ChangeTutorial(bool mode)
    {
        isTutorialOn = mode;
    }
}
