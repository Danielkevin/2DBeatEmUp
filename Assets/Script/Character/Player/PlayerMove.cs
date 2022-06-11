using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CharacterMove
{
    public override void Movement(float moveSpeed)
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;
        transform.position += new Vector3(x, 0, z) * Time.deltaTime;
        if(x!=0 || z != 0)
        {
            charHandler.Animator.SetBool("isRun", true);
        }
        else
        {
            charHandler.Animator.SetBool("isRun", false);
        }
        if (x < 0 && !isFacingLeft)
        {
            Flip();
            isFacingLeft = true;
        }
        else if(x > 0 && isFacingLeft)
        {
            Flip();
            isFacingLeft = false;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
}
