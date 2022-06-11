using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] CharacterHandler playerHandler;
    [SerializeField] List<CharacterHandler> npcList;

    #region AIStuff
    [SerializeField] private float lookRadius = 4f;
    [SerializeField] List<NavMeshAgent> agentList;
    [SerializeField] TeamManager teamManager;
    #endregion

    //IF YOU OPEN THIS, Belum bisa ngejar player, ada warning NAV MESH NYA ENGGA BENAR

    private void Start()
    {
        playerHandler.HealthPoint = playerHandler.CharData.HealthPoint;
        //foreach (CharacterHandler npc in npcList)
        //{
        //    agentList.Add(npc.GetComponent<NavMeshAgent>());
        //}
    }

    void Update()
    {
        PlayerMove();
        NpcMove();
    }

    private void PlayerMove() => playerHandler.PlayerMovement();

    //Function di bawah ini merupakan function untuk menggerakan npc baik tim merah maupun putih.
    #region NPC_Move
    private void NpcMove()
    {
        if (npcList != null)
        {
            for (int i = 0; i < npcList.Count; i++)
            {
                if (npcList[i].GetHealth() > 0)
                {

                    npcList[i].CurrTarget = GenerateCurrTarget(npcList[i].transform);
                    //Movement is below
                    float distance = (npcList[i].CurrTarget.position - npcList[i].transform.position).magnitude;
                    //Debug.Log(distance);
                    if (distance <= lookRadius)
                    {
                        if (agentList[i].gameObject.activeSelf)
                        {
                            agentList[i].SetDestination(npcList[i].CurrTarget.position);
                            Vector3 targetPosition = npcList[i].CurrTarget.position - npcList[i].transform.position;
                            //Debug.Log(npcList[i].transform.name + " Facing towards ==> " + currTarget.name + " With X Coordinate ==> " 
                            //    + targetPosition.normalized.x);
                            //Flip if statement
                            if (targetPosition.normalized.x < 0 && npcList[i].CharMove.IsFacingLeft == false)
                            {
                                npcList[i].CharFlip();
                                npcList[i].CharMove.IsFacingLeft = true;
                            }
                            else if (targetPosition.normalized.x > 0 && npcList[i].CharMove.IsFacingLeft == true)
                            {
                                npcList[i].CharFlip();
                                npcList[i].CharMove.IsFacingLeft = false;
                            }

                            if (distance <= 1)
                            {
                                Attack(i);
                            }
                        }
                    }
                    else
                    {
                        npcList[i].CurrTarget = null;
                    }
                }
                else
                {
                    if (npcList != null)
                    {
                        npcList[i].gameObject.SetActive(false);
                        Debug.Log("Kill ==> " + npcList[i].name);
                        KillActiveNPC(npcList[i]);
                    }
                }
            }
        }
    }

    [SerializeField] List<float> distanceList;
    private Transform GenerateCurrTarget(Transform NPCTransform)
    {
        if (distanceList != null)
        {
            distanceList.Clear();
        }
        if (NPCTransform.tag.Equals("Blue"))
        {
            for (int i = 0; i < teamManager.RedTeam.Count; i++)
            {
                float distance = (teamManager.RedTeam[i].transform.position - NPCTransform.position).magnitude;
                distanceList.Add(distance);
            }
            int index = MinValue();
            //Debug.Log("Closest Target is " + teamManager.RedTeam[index].name + " Requested by " + NPCTransform.name);
            return teamManager.RedTeam[index].transform;
        }
        else if (NPCTransform.tag.Equals("Red"))
        {
            for (int i = 0; i < teamManager.BlueTeam.Count; i++)
            {
                float distance = (teamManager.BlueTeam[i].transform.position - NPCTransform.position).magnitude;
                distanceList.Add(distance);
            }
            int index = MinValue();
            //Debug.Log("Closest Target is " + teamManager.BlueTeam[index].name + " Requested by " + NPCTransform.name);
            return teamManager.BlueTeam[index].transform;
        }
        return null;

        //for (int i = 0; i < targetList.Count; i++)
        //{
        //    if (targetList[i].tag != NPCTransform.tag && targetList[i] != NPCTransform)
        //    {
        //        float distance = (NPCTransform.position - targetList[i].position).magnitude;
        //        distanceList.Add(distance);
        //    }
        //}
    }

    int MinValue()
    {
        int pos = 0;
        for (int j = pos + 1; j < distanceList.Count; j++)
        {
            if (distanceList[pos] > distanceList[j])
                pos = j;
        }
        return pos;
    }

    public void AddActiveNPC(CharacterHandler character)
    {
        agentList.Add(character.GetComponent<NavMeshAgent>());
        npcList.Add(character);
    }
    public void KillActiveNPC(CharacterHandler character) 
    {
        if(character.tag.Equals("Blue"))
        {
            teamManager.BlueTeam.Remove(character.gameObject);
        }
        else if(character.tag.Equals("Red"))
        {
            teamManager.RedTeam.Remove(character.gameObject);
        }
        agentList.Remove(character.GetComponent<NavMeshAgent>());
        npcList.Remove(character); 
    }

    void Attack(int index)
    {
        float selisihWaktu = Time.time - npcList[index].LastTimeAtk;
        if (selisihWaktu > npcList[index].CharData.RateOfAtk)
        {
            npcList[index].CharAttack();
            npcList[index].LastTimeAtk = Time.time;
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    foreach(CharacterHandler npc in npcList)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(npc.transform.position, lookRadius);
    //    }
    //}

    #endregion
}
