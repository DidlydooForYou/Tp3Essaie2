using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

public class Teleport : Node
{
    private Transform self;
    private Transform target;
    private float rangeTeleport;
    private float offsetPlayer;

    private NavMeshAgent agent;

    public Teleport(Transform self, Transform target, float rangeTeleport, float offsetPlayer, NavMeshAgent agent, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.self = self;
        this.target = target;
        this.rangeTeleport = rangeTeleport;
        this.offsetPlayer = offsetPlayer;
        this.agent = agent;
    }

    public override void ExecuteAction()
    {
        float d = Vector3.Distance(self.transform.position, target.position);
        if (d < rangeTeleport)
        {
            FinishAction(false);
            return;
        }
        Vector3 teleportPos = target.position - target.forward * offsetPlayer;

        if (agent)
        {
            agent.Warp(teleportPos);
        }

        base.ExecuteAction();
    }
    public override void Tick(float deltaTime)
    {
        FinishAction(true);
    }

    public override void FinishAction(bool result)
    {
        base.FinishAction(result);
    }
}
