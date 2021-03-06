using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask hostileLayers;
    [SerializeField] CharacterHandler charHandler;
    List<Collider> charHitList;

    public virtual void MeleeAttack()
    {
        charHitList = new List<Collider>(Physics.OverlapSphere(attackPoint.position, attackRange, hostileLayers));
        foreach(Collider hostile in charHitList)
        {
            CharacterHandler charComponent = hostile.GetComponent<CharacterHandler>();
            if(charComponent != null)
                charComponent.GetHit(charHandler.CharData.AtkDmg);
            Debug.Log("HIT!!  ==>   " + hostile.name + " By " + this.name);
        }
        if (charHitList == null)
            charHandler.PlaySound("Miss");
        else
            charHandler.PlaySound("Hit");
        charHitList.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
