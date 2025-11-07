using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

public class Teleport : Node
{
    private Transform self;
    private Transform target;
    private float rangeTeleport;
    private float offsetPlayer;

    float cooldown = 4f;
    float nextUseTime = 0f;

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

        if (Time.time < nextUseTime)
        {
            FinishAction(false);
            return;
        }

        Vector3 teleportPos = target.position - target.forward * offsetPlayer;

        if (agent)
        {
            agent.Warp(teleportPos);
        }
        else
        {
            self.position = teleportPos;
        }

        nextUseTime = Time.time + cooldown;

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
