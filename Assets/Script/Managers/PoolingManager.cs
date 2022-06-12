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

    public CharacterHandler spawnBlue(Transform spawnPositionA)
    {
        CharacterHandler blueArmy = bluePool[0];
        blueArmy.transform.position = spawnPositionA.position;
        blueArmy.transform.rotation = Quaternion.Euler(0,0,0);
        blueArmy.CharMove.IsFacingLeft = false;
        blueArmy.CurrTarget = null;
        blueArmy.HealthPoint = blueArmy.CharData.HealthPoint;
        bluePool.Remove(blueArmy);
        bluePool.Add(blueArmy);
        blueArmy.gameObject.SetActive(true);
        charControl.AddActiveNPC(blueArmy);
        return blueArmy;
    }
    
    public CharacterHandler spawnRed(Transform spawnPositionB)
    {
        CharacterHandler redArmy = redPool[0];
        redArmy.transform.position = spawnPositionB.position;
        redArmy.transform.rotation = Quaternion.Euler(0, 0, 0);
        redArmy.CharMove.IsFacingLeft = false;
        redArmy.CurrTarget = null;
        redArmy.HealthPoint = redArmy.CharData.HealthPoint;
        redPool.Remove(redArmy);
        redPool.Add(redArmy);
        redArmy.gameObject.SetActive(true);
        //Debug.Log("Spawn Position => " + spawnPositionB.position);
        charControl.AddActiveNPC(redArmy);
        return redArmy;
    }
}
