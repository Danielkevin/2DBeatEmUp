using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    [SerializeField] CharacterData charData; //Get data player
    [SerializeField] CharacterMove charMove; //Get player move Script, can call function through here
    [SerializeField] CharacterAttack playerAttack; //Get player attack, can access function through here

    public CharacterData CharData { get => charData; set => charData = value; }
    public CharacterMove CharMove { get => charMove; set => charMove = value; }
    public CharacterAttack PlayerAttack1 { get => playerAttack; set => playerAttack = value; }

    #region Variables

    #endregion

    #region Functions
    public void PlayerMovement() => charMove.Movement(charData.MovementSpeed);
    public void PlayerAttack() => playerAttack.MeleeAttack();
    public void CharFlip() => charMove.Flip();
    #endregion
}
