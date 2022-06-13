using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject LoseMenu;
    [SerializeField] private GameObject WinMenu;

    public void StartGame() 
    {
        ResumeGame();
        SceneManager.LoadScene(1); 
    }

    public void QuitGame() => Application.Quit();

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void LoadMenu() 
    {
        ResumeGame();
        SceneManager.LoadScene(0); 
    }

    public void Defeated()
    {
        LoseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Victory()
    {
        WinMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public bool GetPauseBool() => isPaused;
}
