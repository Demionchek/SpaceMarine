using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AttackState : EnemyStates
{
    public AttackState(EnemyMovement enemyMovement) : base(enemyMovement)
    {
    }

    public override IEnumerator CurrentState()
    {
        if (_enemyMovement.IsRange)
        {
            _enemyMovement.Animator.SetBool("Shoot", true);
            _enemyMovement.Animator.SetBool("Idle", false);
            _enemyMovement.Animator.SetBool("Run", false);
            _enemyMovement.transform.LookAt(_enemyMovement.Target);
        }
        else
        {
            _enemyMovement.Animator.SetBool("Attack", true);
            _enemyMovement.Animator.SetBool("Idle", false);
            _enemyMovement.Animator.SetBool("Run", false);
            _enemyMovement.Animator.SetBool("Burst", false);
            _enemyMovement.transform.LookAt(_enemyMovement.Target);
        }
        yield break;
    }
}
