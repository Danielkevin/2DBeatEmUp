using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthHandler : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public HealthBar HealthBar { get => healthBar; set => healthBar = value; }

    public void SetUpHealth(float health) => healthBar.SetMaxHealth(health);
    public float GetHit(float healthPoint, float damage)
    {
        healthPoint = healthPoint - damage;
        healthBar.SetHealth(healthPoint);
        return healthPoint;
    }
    public void SetHealthBarEnabled() => healthBar.gameObject.SetActive(true);
    public void SetHealthBarInabled() => healthBar.gameObject.SetActive(false);
}
