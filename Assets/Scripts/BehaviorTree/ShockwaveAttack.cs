using UnityEngine;
using UnityEngine.AI;

public class ShockwaveAttack : Node
{
    GameObject attackObject;
    GameObject owner;
    Transform target;

    private float attackDuration = 1f;
    private float attackTimer = 5f;
    private bool isAttacking = false;
    private float attackRange;
    private NavMeshAgent agent;
    private float cooldown;

    public float damage = 10f;

    private float nextUseTime = 20f;

    public ShockwaveAttack(GameObject owner, GameObject attackObject, Transform target, float attackRange,float cooldown, NavMeshAgent agent, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.attackObject = attackObject;
        this.owner = owner;
        this.target = target;
        this.attackRange = attackRange;
        this.agent = agent;
        this.cooldown = cooldown;
    }

    public override void ExecuteAction()
    {
        Debug.Log(nextUseTime);
        Debug.Log(Time.time);
        if (Time.time < nextUseTime)
        {
            FinishAction(false);
            return;
        }

        nextUseTime = Time.time + cooldown;

        isAttacking = true;
        attackTimer = attackDuration;
        var shockwave = GameObject.Instantiate(attackObject, target.position, Quaternion.identity);

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
        if (isAttacking)
        {
            attackTimer -= deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
                FinishAction(true);
            }
        }
    }

    public override void FinishAction(bool result)
    {
        if (agent)
        {
            agent.isStopped = false;
        }
        base.FinishAction(result);
    }
}
