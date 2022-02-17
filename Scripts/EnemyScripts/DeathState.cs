using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyStates
{
    public DeathState(EnemyMovement enemyMovement) : base(enemyMovement)
    {
    }

    public override IEnumerator CurrentState()
    {
        if (!_enemyMovement.IsDead)
        {
            int r = Random.Range(0, 2);

            if (r == 0)
            {
                _enemyMovement.Animator.SetTrigger("Death1");
            }
            else
            {
                _enemyMovement.Animator.SetTrigger("Death2");
            }
            _enemyMovement.IsDead = true;
            _enemyMovement.NavMeshAgent.enabled = false;
            _enemyMovement.CapsuleCollider.enabled = false;
            _enemyMovement.BoxCollider.enabled = false;
            _enemyMovement.Drop();
        }
        yield break;
    }
}
