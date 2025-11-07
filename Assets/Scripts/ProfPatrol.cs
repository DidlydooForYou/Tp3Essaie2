using UnityEngine;
using UnityEngine.AI;

public class BadPatrol : BehaviorTree
{
    [SerializeField] Transform[] targets;
    [SerializeField] GameObject player;

    Interrupt interruptPatrol;
    Interrupt interruptAttack;
    protected override void InitializeTree()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        var goTo1 = new GoToTarget(agent, targets[0], 2, null, this);
        var wait1 = new Wait(2, null, this);
        var goTo2 = new GoToTarget(agent, targets[1], 2, null, this);

        Conditions[] chaseConditions =
        {
            new HasVision(gameObject.transform, player, 60, false),
            new InRange(gameObject.transform, player, 20, false)
        };

        Conditions[] attackConditions =
        {
            new HasVision(gameObject.transform, player, 60, false),
            new InRange(gameObject.transform, player, 2, false)
        };

        var chase = new GoToTarget(agent, player.transform, 1, chaseConditions, this);
        var attack = new Attack(Vector3.forward, Vector3.zero, new Vector3(1, 1, 1), player.tag, gameObject.transform, attackConditions, this);

        var meleeSequence = new Sequence(new Node[] { chase, attack }, null, this);
        var patrolSquence = new Sequence(new Node[] { goTo1, wait1, goTo2, wait1 }, null, this);

        root = new Selector(new Node[] {patrolSquence, meleeSequence}, null, this);

        interruptPatrol = new Interrupt(new Conditions[] { new HasVision(gameObject.transform, player, 15) }, this);

        interruptAttack = new Interrupt(new Conditions[] { new InRange(gameObject.transform, player, 20, true) }, this);
    }

    private void OnDisable()
    {
        interruptPatrol.Stop();
        interruptAttack.Stop();
    }

    private void OnEnable()
    {
        if (interruptAttack != null && interruptPatrol != null)
        {
            interruptAttack.Start();
            interruptPatrol.Start();
        }
    }
}
