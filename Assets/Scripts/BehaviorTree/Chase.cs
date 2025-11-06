using UnityEngine;
using UnityEngine.AI;

public class Chase : Node
{

    private Transform target;
    private float stoppingDistance;
    private NavMeshAgent agent;
    private float meleeRange;
    private float maxChaseDistance = 15f;
    public Chase(Transform target, float stoppingDistance, float meleeRange, NavMeshAgent agent, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.target = target;
        this.stoppingDistance = stoppingDistance;
        this.agent = agent;
        this.meleeRange = meleeRange;
    }

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        if (agent && target)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }

    public override void Tick(float deltaTime)
    {
        if (agent.remainingDistance > maxChaseDistance)
        {
            Debug.Log("out of bounds");
            FinishAction(false);
            return;
        }

        if (!agent || !target)
        {
            FinishAction(false);
            return;
        }
        agent.isStopped = false;

        if (agent.destination != target.position)
            agent.SetDestination(target.position);

        if (agent.remainingDistance <= meleeRange + 0.05f)
        {
            agent.isStopped = true;
            agent.ResetPath();
            FinishAction(true);
            return;
        }

        agent.isStopped = false;
    }

    public override void FinishAction(bool result)
    {
        agent.SetDestination(agent.transform.position);
        base.FinishAction(result);
    }

}
