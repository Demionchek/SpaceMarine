using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class RunState : EnemyStates
{
    public RunState(EnemyMovement enemyMovement) : base(enemyMovement)
    {
    }

    public override IEnumerator CurrentState()
    {
        _enemyMovement.NavMeshAgent.isStopped = false;
        _enemyMovement.NavMeshAgent.SetDestination(_enemyMovement.Target.position);
        _enemyMovement.NavMeshAgent.speed = _enemyMovement.NormalNavSpeed;
        _enemyMovement.Animator.SetBool("Run", true);
        _enemyMovement.Animator.SetBool("Idle", false);
        _enemyMovement.Animator.SetBool("Attack", false);
        _enemyMovement.Animator.SetBool("Shoot", false);
        _enemyMovement.Animator.SetBool("Burst", false);
        yield break;
    }
}

