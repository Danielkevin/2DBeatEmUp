using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float maxRedCount;
    [SerializeField] private List<Transform> redSpawnPosition;
    [SerializeField] private float maxBlueCount;
    [SerializeField] private List<Transform> blueSpawnPosition;

    public List<Transform> RedSpawnPosition { get => redSpawnPosition; set => redSpawnPosition = value; }
    public List<Transform> BlueSpawnPosition { get => blueSpawnPosition; set => blueSpawnPosition = value; }
    public float MaxRedCount { get => maxRedCount; set => maxRedCount = value; }
    public float MaxBlueCount { get => maxBlueCount; set => maxBlueCount = value; }

    #region Spawn_System

    #endregion
}
