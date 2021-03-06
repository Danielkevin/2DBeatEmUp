using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    #region Variables
    [SerializeField] CharacterData charData; //Get data player
    [SerializeField] CharacterMove charMove; //Get player move Script, can call function through here
    [SerializeField] CharacterAttack attack; //Get player attack, can access function through here
    [SerializeField] CharacterHealthHandler healthHandler; //Get player attack, can access function through here
    private float lastTimeAtk;
    [SerializeField] private float healthPoint;
    [SerializeField] private Animator animator;
    private Transform currTarget;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private AudioManager audioManager;

    public CharacterData CharData { get => charData; set => charData = value; }
    public CharacterMove CharMove { get => charMove; set => charMove = value; }
    public float LastTimeAtk { get => lastTimeAtk; set => lastTimeAtk = value; }
    public float HealthPoint { get => healthPoint; set => healthPoint = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public Transform CurrTarget { get => currTarget; set => currTarget = value; }
    #endregion

    #region Functions
    public void PlayerMovement() => charMove.Movement(charData.MovementSpeed);
    public void CharAttack()
    {
        attack.MeleeAttack();
        Debug.Log("Attack System Called");
    }
    public void CharFlip() => charMove.Flip();
    public void SetUpHealth() => healthHandler.SetUpHealth(charData.HealthPoint);
    public void SetUpAnimator() => animator.runtimeAnimatorController = charData.CharAnimator;
    public void SetUpScale() => transform.localScale = charData.Size;
    public void PlaySound(string name) => audioManager.Play(name);
    public void StopSound(string name) => audioManager.Stop(name);
    public bool GetIsPlaySound(string name) => audioManager.GetIsPlay(name);
    public void GetHit(float damage)
    { 
        healthPoint = healthHandler.GetHit(healthPoint, damage);
        animator.SetTrigger("isHurt");
    }
    public float GetHealth() => healthPoint;
    public void SetRun() => animator.SetBool("isRun", true);
    public void SetNotRun() => animator.SetBool("isRun", false);
    public void SetAttack() => animator.SetTrigger("isAttack");
    public void SetHurt() => animator.SetTrigger("isHurt");
    public void HideHealthBar() => healthHandler.SetHealthBarInabled();
    public void ShowHealthBar() => healthHandler.SetHealthBarEnabled();
    public void SetEnableAttack() => CharMove.IsDissableAttack = false;
    public void SetDead()
    {
        PlaySound("Kill");
        PlaySound("Ded");
        animator.SetBool("isDead", true);
        boxCollider.enabled = false;
        currTarget = null;
    }
    public void SetNotDead()
    {
        animator.SetBool("isDead", false);
        boxCollider.enabled = true;
    }
    public void Decay() => StartCoroutine(Decaying());
    IEnumerator Decaying()
    {
        yield return new WaitForSeconds(5);
        SetNotDead();
        this.gameObject.SetActive(false);
    }
    #endregion
}
