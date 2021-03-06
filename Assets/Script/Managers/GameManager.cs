using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxRedCount;
    [SerializeField] private List<Transform> redSpawnPosition;
    [SerializeField] private int maxBlueCount;
    [SerializeField] private List<Transform> blueSpawnPosition;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private PoolingManager poolingManager;
    [SerializeField] private TeamManager teamManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private AudioManager audioManager;
    private int redCasualities = 0;
    [SerializeField] private int maxKilledRed;
    private bool isWin;
    private bool isLose;

    public int MaxRedCount { get => maxRedCount; set => maxRedCount = value; }
    public int MaxBlueCount { get => maxBlueCount; set => maxBlueCount = value; }
    public MenuManager MenuManager { get => menuManager; set => menuManager = value; }

    private void Start()
    {
        StartCoroutine(SpawnTrigger());
        menuManager.UpdateScore(redCasualities);
    }

    public void AddCasualities()
    {
        redCasualities++;
        menuManager.UpdateScore(redCasualities);
        if(redCasualities>=maxKilledRed)
        {
            WinCondition();
        }
    }

    void WinCondition()
    {
        Debug.Log("Win");
        isWin = true;
        StartCoroutine(SetWin());
    }

    public void LoseCondition()
    {
        Debug.Log("Lose");
        isLose = true;
        StartCoroutine(SetLose());
    }

    public void SetPauseInput()
    {
        if (menuManager.GetPauseBool() == false)
            menuManager.PauseGame();
        else if (menuManager.GetPauseBool() == true)
            menuManager.ResumeGame();
    }
    
    IEnumerator SetLose()
    {
        yield return new WaitForSeconds(5);
        menuManager.Defeated();
    }

    IEnumerator SetWin()
    {
        yield return new WaitForSeconds(0.5f);
        menuManager.Victory();
    }

    IEnumerator SpawnTrigger()
    {
        if (!isWin && !isLose)
        {
            if (teamManager.BlueTeam.Count < MaxBlueCount)
            {
                //Debug.Log("Blue Team Total ==> " + teamManager.BlueTeam.Count);
                Spawn("Blue");
            }
            if (teamManager.RedTeam.Count < MaxRedCount)
            {
                //Debug.Log("Red Team Total ==> " + teamManager.RedTeam.Count);
                Spawn("Red");
            }
        }
        yield return new WaitForSeconds(timeToSpawn);
        StartCoroutine(SpawnTrigger());
    }

    #region Spawn_System
    int ii = 0;
    int ij = 0;
    void Spawn(string team)
    {
        if(team.Equals("Blue"))
        {
            poolingManager.SpawnNPC(blueSpawnPosition[ii], team);
            ii++;
            if (ii >= blueSpawnPosition.Count)
            {
                ii = 0;
            }
        }
        if(team.Equals("Red"))
        {
            poolingManager.SpawnNPC(redSpawnPosition[ij], team);
            //Debug.Log("Spawned on ==> " + redSpawnPosition[ij].name);
            ij++;
            if (ij >= redSpawnPosition.Count)
            {
                ij = 0;
            }
        }
    }

    //IEnumerator SpawnBlue(int index)
    //{
    //    yield return new WaitForSeconds(timeToSpawn);
    //    poolingManager.spawnBlue(blueSpawnPosition[index]);
    //}
    //IEnumerator SpawnRed(int index)
    //{
    //    yield return new WaitForSeconds(timeToSpawn);
    //    poolingManager.spawnRed(redSpawnPosition[index]);
    //}
    #endregion
}
