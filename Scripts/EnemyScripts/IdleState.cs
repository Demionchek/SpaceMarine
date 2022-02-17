using System.Collections;

public class IdleState : EnemyStates
{
    public IdleState(EnemyMovement enemyMovement) : base(enemyMovement)
    {
    }

    public override IEnumerator CurrentState()
    {
        _enemyMovement.NavMeshAgent.enabled = true;
        _enemyMovement.CapsuleCollider.enabled = true;
        _enemyMovement.BoxCollider.enabled = true;
        _enemyMovement.NavMeshAgent.isStopped = true;
        _enemyMovement.Animator.SetBool("Idle", true);
        _enemyMovement.Animator.SetBool("Burst", false);
        _enemyMovement.Animator.SetBool("Run", false);
        _enemyMovement.Animator.SetBool("Attack", false);
        _enemyMovement.Animator.SetBool("Shoot", false);


        yield break;
    }
}

