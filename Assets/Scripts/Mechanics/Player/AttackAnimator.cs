using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttack))]
public class AttackAnimator : MonoBehaviour
{
    PlayerMovement PM;

    PlayerAttack PA;

    Animator anim;

    private void Awake()
    {
        PA = GetComponent<PlayerAttack>();
        PM = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();

        PA.onAttack += OnAttack;
    }

    public void OnAttack()
    {
        anim.ResetTrigger("Attack");
        anim.SetInteger("Direction", PM.Rotation);
        anim.SetTrigger("Attack");
    }
}
