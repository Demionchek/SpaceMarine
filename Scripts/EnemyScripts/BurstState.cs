using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstState : EnemyStates
{
    public BurstState(EnemyMovement enemyMovement) : base(enemyMovement)
    {
    }

    public override IEnumerator CurrentState()
    {
        if (_enemyMovement.BurstCooled)
        {
            _enemyMovement.IsBursting = true;
            _enemyMovement.Animator.SetBool("Burst", true);
            _enemyMovement.Animator.SetBool("Idle", false);
            _enemyMovement.Animator.SetBool("Run", false);
            _enemyMovement.Animator.SetBool("Attack", false);
            _enemyMovement.transform.LookAt(_enemyMovement.LastTargetPos);
            _enemyMovement.NavMeshAgent.SetDestination(_enemyMovement.LastTargetPos);
            _enemyMovement.NavMeshAgent.speed = _enemyMovement.BurstSpeed;
            _enemyMovement.BurstCooled = false;
        }
        yield break;
    }
}
