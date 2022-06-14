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
            if(charHandler.GetIsPlaySound("Run") == false)
                charHandler.PlaySound("Run");
        }
        else
        {
            charHandler.Animator.SetBool("isRun", false);
                charHandler.StopSound("Run");
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
        if(Input.GetButtonDown("Fire1") && isDissableAttack == false)
        {
            isDissableAttack = true;
            charHandler.SetAttack();
            //Attack();
            Debug.Log("Player call Attack system");
        }
    }
}
