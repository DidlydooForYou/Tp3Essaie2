using UnityEngine;
using UnityEngine.AI;

public class RangedAttack : Node
{
    GameObject attackObject;
    GameObject owner;
    Transform target;

    readonly Transform firePoint;

    private bool isAttacking = false;
    private float attackRange;
    private float minAttackRange;

    private NavMeshAgent agent;

    private float attackDuration = .5f;
    private float attackTimer = 0f;

    private float damage = 10f;

    public RangedAttack(GameObject owner, GameObject attackObject, Transform target, Transform firePoint, float attackRange,float minAttackRange, NavMeshAgent agent, Conditions[] conditions, BehaviorTree BT) : base(conditions,BT)
    {
        this.attackObject = attackObject;
        this.owner = owner;
        this.target = target;
        this.firePoint = firePoint;
        this.attackRange = attackRange;
        this.agent = agent;
        this.minAttackRange = minAttackRange;
    }

    public override void ExecuteAction()
    {
        float d = Vector3.Distance(owner.transform.position, target.position);

        if (d > attackRange + 0.1f || d < minAttackRange)
        {
            FinishAction(false);
            return;
        }
        isAttacking = true;
        attackTimer = attackDuration;
        var projectile = GameObject.Instantiate(attackObject, firePoint.position, firePoint.rotation);

        base.ExecuteAction();

        var agent = owner.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }

    public override void Tick(float deltaTime)
    {
        float d = Vector3.Distance(owner.transform.position, target.position);
        if (d > attackRange + 0.1f)
        {
            Debug.Log("out of range");
            FinishAction(false);
            return;
        }

        Vector3 to = target.position - owner.transform.position;
        to.y = 0f;
        if (to.sqrMagnitude > 0.001f)
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(to), 10f * deltaTime);

        if (d < minAttackRange)
        {
            Debug.Log("too close");
            FinishAction(false);
            return;
        }
        attackTimer -= deltaTime;

        if (attackTimer < 0f && d > minAttackRange)
        {
            var projectile = GameObject.Instantiate(attackObject, firePoint.position, firePoint.rotation);

            isAttacking = false;

            FinishAction(true);
        }

    }

}
