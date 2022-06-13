using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] protected CharacterHandler charHandler;
    [SerializeField] protected bool isFacingLeft = false;
    protected bool isDissableAttack;
    public bool IsFacingLeft { get => isFacingLeft; set => isFacingLeft = value; }
    public bool IsDissableAttack { get => isDissableAttack; set => isDissableAttack = value; }

    public virtual void Movement(float moveSpeed)
    {

    }

    public void Flip()
    {
        if(!isFacingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(isFacingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    protected virtual void Attack() => charHandler.CharAttack();
}
