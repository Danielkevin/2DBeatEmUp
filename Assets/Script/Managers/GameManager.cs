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

    public List<Transform> RedSpawnPosition { get => redSpawnPosition; set => redSpawnPosition = value; }
    public List<Transform> BlueSpawnPosition { get => blueSpawnPosition; set => blueSpawnPosition = value; }
    public int MaxRedCount { get => maxRedCount; set => maxRedCount = value; }
    public int MaxBlueCount { get => maxBlueCount; set => maxBlueCount = value; }
    public PoolingManager PoolingManager { get => poolingManager; set => poolingManager = value; }
    public TeamManager TeamManager { get => teamManager; set => teamManager = value; }

    private void Start()
    {
        StartCoroutine(SpawnTrigger());
    }


    IEnumerator SpawnTrigger()
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
            poolingManager.spawnBlue(blueSpawnPosition[ii]);
            ii++;
            if (ii >= blueSpawnPosition.Count)
            {
                ii = 0;
            }
        }
        if(team.Equals("Red"))
        {
            poolingManager.spawnRed(redSpawnPosition[ij]);
            //Debug.Log("Spawned on ==> " + redSpawnPosition[ij].name);
            ij++;
            if (ij >= redSpawnPosition.Count)
            {
                ij = 0;
            }
        }
        //switch(team)
        //{
        //    case "Blue":
        //poolingManager.spawnBlue(blueSpawnPosition[ii]);
        //ii++;
        //if (ii >= blueSpawnPosition.Count)
        //{
        //    ii = 0;
        //}
        //        break;
        //    case "Red":
        //poolingManager.spawnRed(redSpawnPosition[ij]);
        //Debug.Log("Spawned on ==> " + redSpawnPosition[ij].name);
        //ij++;
        //if (ij >= redSpawnPosition.Count)
        //{
        //    ij = 0;
        //}
        //        break;
        //}
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
