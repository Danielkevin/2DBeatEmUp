using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    [SerializeField] CharacterData playerData; //Get data player
    [SerializeField] PlayerMove playerMove;

    #region Variables
    //private float movementSpeed;
    //public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    #endregion

    #region Functions
    public void PlayerMovement() => playerMove.Movement(playerData.MovementSpeed);
    #endregion

    private void Update()
    {
        PlayerMovement();
    }
}
