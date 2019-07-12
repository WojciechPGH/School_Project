using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScripts : MonoBehaviour
{
    ReturnToMenu rtm;
    void Start()
    {
        rtm = FindObjectOfType<ReturnToMenu>();
    }

    public void Return()
    {
        rtm.ReturnToMainMenu();
    }

    public void Restart()
    {
        rtm.RestartGame();
    }
}
