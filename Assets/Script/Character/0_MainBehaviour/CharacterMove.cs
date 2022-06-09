using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] protected CharacterHandler charHandler;
    protected bool isFacingLeft = false;
    public bool IsFacingLeft { get => isFacingLeft; set => isFacingLeft = value; }

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

    protected virtual void Attack() => charHandler.PlayerAttack();
}
