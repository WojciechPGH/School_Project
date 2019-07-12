using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    bool gameIsOver = false;
    private int currentLevel = 0;
    private static int singleton = 0;

    private void Awake()
    {
        singleton++;
        if (singleton > 1)
        {
            Destroy(gameObject);
            singleton--;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += OnLevelLoaded;
        }

    }

    void Start()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (currentLevel > 0 && currentLevel != 4)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && gameIsOver == false)
            {
                if (!pauseScreen.activeSelf)
                {
                    PauseGame();
                }
                else
                if (pauseScreen.activeSelf)
                {
                    ContinueGame();
                }
            }
        }
        else
        {
            gameIsOver = false;
        }
    }

    private void OnLevelLoaded(Scene current, Scene next)
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void GameOver()
    {
        gameIsOver = true;
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
    public void RestartGame()
    {
        if (currentLevel == 1)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(2);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel < 4)
            SceneManager.LoadScene(++currentLevel);
    }
}
