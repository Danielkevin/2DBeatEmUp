using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [SerializeField] CharacterControl charControl;

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

    public CharacterHandler spawnBlue()
    {
        CharacterHandler blueArmy = bluePool[0];
        blueArmy.HealthPoint = blueArmy.CharData.HealthPoint;
        bluePool.Remove(blueArmy);
        bluePool.Add(blueArmy);
        blueArmy.gameObject.SetActive(true);
        charControl.AddActiveNPC(blueArmy); 
        return blueArmy;
    }
    
    public CharacterHandler spawnRed()
    {
        CharacterHandler redArmy = redPool[0];
        redArmy.HealthPoint = redArmy.CharData.HealthPoint;
        redPool.Remove(redArmy);
        redPool.Add(redArmy);
        redArmy.gameObject.SetActive(true);
        charControl.AddActiveNPC(redArmy);
        return redArmy;
    }
}
