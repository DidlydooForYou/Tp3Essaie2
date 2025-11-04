using UnityEngine;
using UnityEngine.AI;

public class BadPatrol : BehaviorTree
{
    [SerializeField] Transform[] targets;
    Interrupt interrupt;
    protected override void InitializeTree()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        interrupt = new Interrupt(new Conditions[] { new HasVision(gameObject.transform, gameObject, 15, false) }, this);

        GoToTarget goTo1 = new GoToTarget(agent, targets[0], 1, null, this);
        Wait wait1 = new Wait(2, null, this);
        Wait wait2 = new Wait(2, null, this);
        GoToTarget goTo2 = new GoToTarget(agent, targets[1], 1, null, this);

        root = new Sequence(new Node[] { goTo1, wait1, goTo2, wait2 }, null, this);
    }

    private void OnDisable()
    {
        interrupt.Stop();
    }

    private void OnEnable()
    {
        if (interrupt != null)
            interrupt.Start();
    }
}