using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField] private float movementSpeed;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
}
