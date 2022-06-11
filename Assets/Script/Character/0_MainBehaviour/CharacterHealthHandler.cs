using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthHandler : MonoBehaviour
{
    public float GetHit(float healthPoint, float damage)
    {
        healthPoint = healthPoint - damage;
        return healthPoint;
    }
}
