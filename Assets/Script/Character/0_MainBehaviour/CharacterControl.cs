using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] CharacterHandler playerHandler;
    [SerializeField] List<CharacterHandler> npcList;

    // Update is called once per frame
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
        float firstPos;
        for (int i = 0; i < npcList.Count; i++)
        {
            //please change this later
            firstPos = npcList[i].transform.position.x;
            npcList[i].transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
            if(npcList[i].transform.position.x - firstPos < 0 && npcList[i].CharMove.IsFacingLeft == false)
            {
                npcList[i].CharFlip();
                npcList[i].CharMove.IsFacingLeft = true;
            }
            else if(npcList[i].transform.position.x - firstPos > 0 && npcList[i].CharMove.IsFacingLeft == true)
            {
                npcList[i].CharFlip();
                npcList[i].CharMove.IsFacingLeft = false;
            }
        }
    }
    #endregion
}
