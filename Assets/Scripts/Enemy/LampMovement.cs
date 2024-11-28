public class LampMovement : EnemyMovement
{
    private LampAttack _lampAttack;

    protected override void Awake()
    {
        base.Awake();
        _lampAttack = GetComponent<LampAttack>();
    }

    protected override void Update()
    {
        base.Update();
        if (_lampAttack.IsAttacking)
        {
            _navMeshAgent.SetDestination(transform.position);
            return;
        }
    }
}