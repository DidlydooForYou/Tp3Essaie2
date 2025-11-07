using UnityEngine;
using UnityEngine.AI;

public class Chase : Node
{

    private Transform target;
    private float stoppingDistance;
    private NavMeshAgent agent;
    private float maxChaseDistance;

    public Chase(Transform target, float stoppingDistance, NavMeshAgent agent, float maxChaseDistance, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.target = target;
        this.stoppingDistance = stoppingDistance;
        this.agent = agent;
        this.maxChaseDistance = maxChaseDistance;
    }

    public override void ExecuteAction()
    {
        if (!agent || !target)
        {
            FinishAction(false);
            return;
        }

        float d = Vector3.Distance(agent.transform.position, target.position);
        if (d > maxChaseDistance)
        {
            FinishAction(false);
            return;
        }

        base.ExecuteAction();

        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    public override void Tick(float deltaTime)
    {
        float d = Vector3.Distance(agent.transform.position, target.position);
        if (d > maxChaseDistance)
        {
            Debug.Log("out of bounds");
            agent.isStopped = true;
            FinishAction(false);
            return;
        }
        Debug.Log(agent.remainingDistance);
        if (!agent || !target)
        {
            FinishAction(false);
            return;
        }
        agent.isStopped = false;

        if (agent.destination != target.position)
            agent.SetDestination(target.position);

        if (d <= stoppingDistance + 0.05f)
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
        agent.ResetPath();
        base.FinishAction(result);
    }

}
