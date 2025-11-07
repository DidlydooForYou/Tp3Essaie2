using UnityEngine;
using UnityEngine.AI;

public class BadPatrol : BehaviorTree
{
    [SerializeField] Transform[] targets;
    [SerializeField] GameObject player;

    Interrupt interrupt;

    protected override void InitializeTree()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //chaseConditions
        HasVision HVChase = new HasVision(gameObject.transform, player, 60, 20, false);
        InRange IRChase = new InRange(gameObject.transform, player, 20f, false);

        //attackCondition
        HasVision HVAttack = new HasVision(gameObject.transform, player, 60, 20, false);
        InRange IRAttack = new InRange(gameObject.transform, player, 2, false);

        //patrolCondition
        HasVision HVPatrol = new HasVision(gameObject.transform, player, 45, 20, true);

        var wait = new Wait(2, new Conditions[] { HVPatrol }, this);
        var goTo1 = new GoToTarget(agent, targets[Random.Range(0, targets.Length)], 2, new Conditions[] {HVPatrol}, this);
        var goTo2 = new GoToTarget(agent, targets[Random.Range(0, targets.Length)], 2, new Conditions[] { HVPatrol }, this);
        var goTo3 = new GoToTarget(agent, targets[Random.Range(0, targets.Length)], 2, new Conditions[] { HVPatrol }, this);
        var goTo4 = new GoToTarget(agent, targets[Random.Range(0, targets.Length)], 2, new Conditions[] { HVPatrol }, this);
        var goTo5 = new GoToTarget(agent, targets[Random.Range(0, targets.Length)], 2, new Conditions[] { HVPatrol }, this);

        var chase = new GoToPlayer(agent, player.transform, 20, 4, 2 , new Conditions[] {HVChase, IRChase}, this);
        var attack = new Attack(Vector3.forward, Vector3.zero, new Vector3(1, 1, 1), player.tag, gameObject.transform, new Conditions[] {HVAttack, IRAttack}, this);

        var meleeSequence = new Sequence(new Node[] { chase, attack }, null, this);
        var patrolSquence = new Sequence(new Node[] { goTo1, wait, goTo2, wait, goTo3, wait, goTo4, wait, goTo5, wait }, null, this);

        root = new Selector(new Node[] { meleeSequence, patrolSquence }, null, this);

        interrupt = new Interrupt(new Conditions[] { HVPatrol }, this);
        interrupt = new Interrupt( new Conditions[] {HVChase,IRChase,HVAttack, IRAttack} , this);
    }

    private void OnDisable()
    {
        interrupt.Stop();
    }

    private void OnEnable()
    {
        if(interrupt  != null) 
            interrupt.Start();
    }
}
