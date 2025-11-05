using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class ComportementBoss : BehaviorTree
{
    [SerializeField] Transform player;
    [SerializeField] GameObject owner;
    [SerializeField] GameObject meleeAttackObject;
    float meleeRange = 15f;
    float rangedRange = 35f;
    float attackRange = 3f;

    private WithinRange withinRangeCondition;
    protected override void InitializeTree()
    {
        if (player != null)
        {
            withinRangeCondition = new WithinRange(transform, player, meleeRange, rangedRange);

            var agent = GetComponent<NavMeshAgent>();

            Conditions[] meleeConditions = new Conditions[] { withinRangeCondition };

            var chase = new Chase(player, attackRange, meleeRange, agent, new Conditions[] { withinRangeCondition }, this);
            var attack = new MeleeAttack(owner, meleeAttackObject, player, attackRange,agent, meleeConditions, this);

            root = new Sequence(new Node[] { chase, attack }, meleeConditions, this);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        Vector3 pos = transform.position;

        // Melee zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, meleeRange);

        // Ranged zone
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pos, rangedRange);

        // Far zone
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, rangedRange * 1.2f);

        if (player != null)
            Gizmos.DrawLine(pos, player.transform.position);
    }
#endif

}
