using UnityEngine;
using UnityEngine.AI;

public class ShockwaveAttack : Node
{
    GameObject attackObject;
    GameObject owner;
    Transform target;

    private float attackDuration = 1f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    private float attackRange;
    private NavMeshAgent agent;

    public float damage = 10f;

    private float cooldown = 1f;
    private float nextUseTime = 0f;

    public ShockwaveAttack(GameObject owner, GameObject attackObject, Transform target, float attackRange, NavMeshAgent agent, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.attackObject = attackObject;
        this.owner = owner;
        this.target = target;
        this.attackRange = attackRange;
        this.agent = agent;
    }

    public override void ExecuteAction()
    {
        base.ExecuteAction();
    }
}
