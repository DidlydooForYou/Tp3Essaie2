using UnityEngine;
using UnityEngine.AI;

public class GoToPlayer : Node
{
    Transform target;
    float surcharge;
    float pause;
    NavMeshAgent agent;
    bool isPaused;
    float stoppingDistance;

    public float timer = 0;

    public GoToPlayer(NavMeshAgent agent, Transform target, float surcharge, float pause, float stoppingDistance, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.agent = agent;
        this.target = target;
        this.surcharge = surcharge;
        this.stoppingDistance = stoppingDistance;
        this.pause = pause;
    }

    public override void ExecuteAction()
    {
        if (!isPaused)
        {
            agent.SetDestination(target.position);
            base.ExecuteAction();
        }
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log(timer);
        timer += deltaTime;

        if (!isPaused)
        {
            if (timer >= surcharge)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                timer = 0;
                isPaused = true;
                return;
            }
        }
        if (isPaused)
        {
            if(timer >= pause)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                timer = 0;
                isPaused = false;
                return;
            }
        }

        if ((agent.transform.position - target.position).sqrMagnitude < stoppingDistance * stoppingDistance)
        {
            FinishAction(true);
        }
        else
        {
            if (!agent.SetDestination(target.position))
            {
                FinishAction(false);
            }
        }
    }

    public override void FinishAction(bool result)
    {
        agent.SetDestination(agent.transform.position);
        base.FinishAction(result);
    }

    public override void Interrupt()
    {
        agent.SetDestination(agent.transform.position);
        base.Interrupt();
    }
}
