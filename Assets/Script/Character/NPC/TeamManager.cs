using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] List<GameObject> blueTeam;
    [SerializeField] List<GameObject> redTeam;

    public List<GameObject> BlueTeam { get => blueTeam; set => blueTeam = value; }
    public List<GameObject> RedTeam { get => redTeam; set => redTeam = value; }

    private void Awake()
    {
        blueTeam = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blue"));
        redTeam = new List<GameObject>(GameObject.FindGameObjectsWithTag("Red"));
    }
}
