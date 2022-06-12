using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [SerializeField] CharacterControl charControl;
    [SerializeField] TeamManager teamManager;

    [SerializeField] int maxCountBlue;
    [SerializeField] CharacterHandler bluePrefab;
    [SerializeField] List<CharacterHandler> bluePool = new List<CharacterHandler>();

    [SerializeField] int maxCountRed;
    [SerializeField] CharacterHandler redPrefab;
    [SerializeField] List<CharacterHandler> redPool = new List<CharacterHandler>();

    private void Awake()
    {
        initiate();
    }

    void initiate()
    {
        for (int i = 0; i < maxCountBlue; i++)
        {
            CharacterHandler blueArmy = Instantiate(bluePrefab);
            blueArmy.gameObject.SetActive(false);
            bluePool.Add(blueArmy);
        }
        for (int i = 0; i < maxCountRed; i++)
        {
            CharacterHandler redArmy = Instantiate(redPrefab);
            redArmy.gameObject.SetActive(false);
            redPool.Add(redArmy);
        }
    }

    public CharacterHandler SpawnNPC(Transform spawnPositionA, string teamTag)
    {
        CharacterHandler army = null;
        switch(teamTag)
        {
            case "Blue":
                army = bluePool[0];
                bluePool.Remove(army);
                bluePool.Add(army);
                break;
            case "Red":
                army = redPool[0];
                redPool.Remove(army);
                redPool.Add(army);
                break;
        }
        army.transform.position = spawnPositionA.position;
        army.transform.rotation = Quaternion.Euler(0,0,0);
        army.SetNotDead();
        army.CharMove.IsFacingLeft = false;
        army.CurrTarget = null;
        army.HealthPoint = army.CharData.HealthPoint;
        army.gameObject.SetActive(true);
        charControl.AddActiveNPC(army);
        return army;
    }
}
