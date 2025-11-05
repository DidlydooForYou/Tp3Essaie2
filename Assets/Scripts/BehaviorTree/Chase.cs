using UnityEngine;
using UnityEngine.AI;

public class Chase : Node
{

    private Transform target;
    private float stoppingDistance;
    private NavMeshAgent agent;
    private float meleeRange;

    public Chase(Transform target, float stoppingDistance, float meleeRange, NavMeshAgent agent, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.target = target;
        this.stoppingDistance = stoppingDistance;
        this.agent = agent;
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
        if (agent.remainingDistance > meleeRange)
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
        agent.SetDestination(target.position);

        bool arrived = !agent.pathPending && agent.remainingDistance <= (Mathf.Max(stoppingDistance, agent.stoppingDistance) + .1) && (!agent.hasPath || agent.velocity.sqrMagnitude <= 0.01f);

        if (arrived)
        {
            agent.ResetPath();
            FinishAction(true);
            return;
        }
    }

    public override void FinishAction(bool result)
    {
        agent.SetDestination(agent.transform.position);
        base.FinishAction(result);
    }

}
