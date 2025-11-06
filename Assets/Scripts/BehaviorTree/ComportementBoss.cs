using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class ComportementBoss : BehaviorTree
{
    [SerializeField] Transform player;
    [SerializeField] GameObject owner;
    [SerializeField] GameObject meleeAttackObject;
    [SerializeField] GameObject rangedAttackObject;
    float meleeRange = 15f;
    float rangedRange = 35f;
    float attackRange = 4.5f;

    private WithinRange withinRangeCondition;
    protected override void InitializeTree()
    {
        if (player != null)
        {
            withinRangeCondition = new WithinRange(transform, player, meleeRange, rangedRange);

            var agent = GetComponent<NavMeshAgent>();

            Conditions[] meleeConditions = new Conditions[] { withinRangeCondition };

            var chase = new Chase(player, attackRange, attackRange, agent,null, this);
            var meleeAttack = new MeleeAttack(owner, meleeAttackObject, player, attackRange,agent, meleeConditions, this);
            var rangedAttack = new RangedAttack(owner, rangedAttackObject, player, owner.transform, rangedRange, meleeRange, agent, meleeConditions, this);

            root = new Sequence(new Node[] { chase, meleeAttack,rangedAttack}, meleeConditions, this);
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
