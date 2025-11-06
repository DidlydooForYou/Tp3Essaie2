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

    protected override void InitializeTree()
    {
        if (player != null)
        {
            var agent = GetComponent<NavMeshAgent>();

            var withinRangeConditionMelee = new WithinRange(transform, player, meleeRange, rangedRange, RangeMode.Melee);
            var withinRangeConditionRanged = new WithinRange(transform, player, meleeRange, rangedRange, RangeMode.Ranged);
            //var withinRangeCondition = new WithinRange(transform, player, meleeRange, rangedRange, RangeMode.Far);

            //melee range
            var chase = new Chase(player, attackRange, agent,null, this);
            var meleeAttack = new MeleeAttack(owner, meleeAttackObject, player, attackRange, agent, null, this);
            var meleeSequence = new Sequence(new Node[] { chase, meleeAttack }, new Conditions[] { withinRangeConditionMelee }, this);

            //ranged range
            var rangedAttack = new RangedAttack(owner, rangedAttackObject, player, owner.transform, rangedRange, meleeRange, agent, new Conditions[] {withinRangeConditionRanged}, this);

            //far range

            //root
            root = new Sequence(new Node[] { meleeSequence,rangedAttack }, null, this);
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
