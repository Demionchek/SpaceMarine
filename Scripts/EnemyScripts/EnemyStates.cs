using System.Collections;

public abstract class EnemyStates
{
    protected EnemyMovement _enemyMovement;

    public EnemyStates(EnemyMovement enemyMovement)
    {
        _enemyMovement = enemyMovement;
    }

    public virtual IEnumerator CurrentState()
    {
        yield break;
    }
}
