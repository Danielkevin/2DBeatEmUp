using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] CharacterHandler playerHandler;
    [SerializeField] Transform patrolPoint;
    [SerializeField] List<CharacterHandler> npcList;
    [SerializeField] GameManager gameManager;

    #region AIStuff
    [SerializeField] private float lookRadius = 4f;
    [SerializeField] List<NavMeshAgent> agentList;
    [SerializeField] TeamManager teamManager;
    #endregion

    public GameManager GameManager { get => gameManager; set => gameManager = value; }

    private void Awake()
    {
        playerHandler.HealthPoint = playerHandler.CharData.HealthPoint;
    }

    private void FixedUpdate()
    {
        PlayerMove();
        NpcMove();
    }

    private void PlayerMove()
    {
        if (playerHandler.GetHealth() > 0)
        {
            playerHandler.PlayerMovement();
        }
        else
        {
            if(playerHandler.Animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit") == false
                && playerHandler.Animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == false)
                playerHandler.SetHurt();
            playerHandler.SetDead();
            Debug.Log("Kill ==> " + playerHandler.name);
            gameManager.LoseCondition();
        }
    }

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
                    float distance = 50;
                    if (npcList[i].CurrTarget != null)
                        distance = (npcList[i].CurrTarget.position - npcList[i].transform.position).magnitude;
                    //Debug.Log(distance);
                    if (distance <= lookRadius )
                    {
                        agentList[i].stoppingDistance = 0.7f;
                        MoveToTarget(npcList[i], i, distance);
                        npcList[i].CurrTarget = null;
                    }
                    else
                    {
                        switch (npcList[i].tag)
                        {
                            case "Blue":
                                npcList[i].CurrTarget = playerHandler.transform;
                                distance = (npcList[i].CurrTarget.position - npcList[i].transform.position).magnitude;
                                agentList[i].stoppingDistance = 3;
                                MoveToTarget(npcList[i], i, distance);
                                break;
                            case "Red":
                                npcList[i].CurrTarget = patrolPoint;
                                distance = (npcList[i].CurrTarget.position - npcList[i].transform.position).magnitude;
                                agentList[i].stoppingDistance = 3;
                                MoveToTarget(npcList[i], i, distance);
                                break;
                        }
                    }
                }
                else
                {
                    if (npcList != null)
                    {
                        npcList[i].SetDead();
                        Debug.Log("Kill ==> " + npcList[i].name);
                        if (npcList[i].tag.Equals("Red"))
                            gameManager.AddCasualities();
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
            if (index <= teamManager.RedTeam.Count && teamManager.RedTeam.Count > 0)
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
            if (index <= teamManager.BlueTeam.Count && teamManager.BlueTeam.Count > 0)
                return teamManager.BlueTeam[index].transform;
        }
        return null;
    }

    void MoveToTarget(CharacterHandler character, int index, float distance)
    {
        if (agentList[index].gameObject.activeSelf)
        {
            agentList[index].SetDestination(character.CurrTarget.position);
            Vector3 targetPosition = character.CurrTarget.position - character.transform.position;
            
            //Flip if statement
            if (targetPosition.normalized.x < 0 && character.CharMove.IsFacingLeft == false)
            {
                character.CharFlip();
                character.CharMove.IsFacingLeft = true;
            }
            else if (targetPosition.normalized.x > 0 && character.CharMove.IsFacingLeft == true)
            {
                character.CharFlip();
                character.CharMove.IsFacingLeft = false;
            }

            //Debug.Log(distance + " <= Distance <= " + agentList[index].name + " => stopping distance => " + agentList[index].stoppingDistance);
            if (distance > agentList[index].stoppingDistance)
                character.SetRun();
            if (distance <= agentList[index].stoppingDistance)
                character.SetNotRun();

            if (character.tag.Equals(character.CurrTarget.tag) == false && distance <= 1 && !character.CurrTarget.tag.Equals("PatrolPoint"))
            {
                Attack(index);
                npcList[index].CurrTarget = null;
            }
        }
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
        if (character.tag.Equals("Blue"))
        {
            teamManager.BlueTeam.Add(character.gameObject);
        }
        else if (character.tag.Equals("Red"))
        {
            teamManager.RedTeam.Add(character.gameObject);
        }
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
            npcList[index].SetAttack();
            //npcList[index].CharAttack();
            Debug.Log("NPC call Attack system");
            npcList[index].LastTimeAtk = Time.time;
            //npcList[index].CurrTarget = null;
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
