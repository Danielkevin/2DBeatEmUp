using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject LoseMenu;
    [SerializeField] private GameObject WinMenu;
    [SerializeField] private TextMeshProUGUI Score;

    public void StartGame() 
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1); 
    }

    public void QuitGame() => Application.Quit();
    public void UpdateScore(int casualities) => Score.text = casualities.ToString();

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Swap();
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Swap();
    }

    public void LoadMenu() 
    {
        ResumeGame();
        SceneManager.LoadScene(0); 
    }

    public void Defeated()
    {
        LoseMenu.SetActive(true);
        Swap();
    }

    public void Victory()
    {
        WinMenu.SetActive(true);
        Swap();
    }

    void Swap()
    {
        isPaused = !isPaused;
        Time.timeScale = (Time.timeScale + 1) % 2;
    }
    public bool GetPauseBool() => isPaused;
}
