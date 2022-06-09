using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask hostileLayers;

    public virtual void MeleeAttack()
    {
        Collider[] charHitList = Physics.OverlapSphere(attackPoint.position, attackRange, hostileLayers);

        foreach(Collider hostile in charHitList)
        {
            Debug.Log("HIT!!  ==>   " + hostile.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
