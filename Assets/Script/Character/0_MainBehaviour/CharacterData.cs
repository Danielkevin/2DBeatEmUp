using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float healthPoint;
    [SerializeField] private float atkDmg;
    [SerializeField] private float rateOfAtk;
    [SerializeField] private RuntimeAnimatorController charAnimator;
    [SerializeField] private Vector3 size;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float HealthPoint { get => healthPoint; set => healthPoint = value; }
    public float AtkDmg { get => atkDmg; set => atkDmg = value; }
    public float RateOfAtk { get => rateOfAtk; set => rateOfAtk = value; }
    public RuntimeAnimatorController CharAnimator { get => charAnimator; set => charAnimator = value; }
    public Vector3 Size { get => size; set => size = value; }
}
