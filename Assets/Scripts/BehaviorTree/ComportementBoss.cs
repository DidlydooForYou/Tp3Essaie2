using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class ComportementBoss : BehaviorTree
{
    [SerializeField] Transform player;

    [SerializeField] GameObject owner;
    [SerializeField] Transform firePoint;

    [SerializeField] GameObject meleeAttackObject;
    [SerializeField] GameObject rangedAttackObject;
    [SerializeField] GameObject shockwaveAttackObject;

    float meleeRange = 15f;
    float rangedRange = 1000f;
    float attackRange = 4.5f;
    float attackRangeShockwave = 10f;
    float offsetPlayer = 5f;

    protected override void InitializeTree()
    {
        if (player != null)
        {
            var agent = GetComponent<NavMeshAgent>();

            var withinRangeConditionMelee = new WithinRange(transform, player, meleeRange, RangeMode.Melee);
            var withinRangeConditionRanged = new WithinRange(transform, player, meleeRange, RangeMode.Ranged);

            //melee range
            var chase = new Chase(player, attackRange, agent, meleeRange, new Conditions[] { withinRangeConditionMelee }, this);
            var meleeAttack = new MeleeAttack(owner, meleeAttackObject, player, attackRange, agent,new Conditions[] {withinRangeConditionMelee}, this);
            var meleeSequence = new Sequence(new Node[] { chase, meleeAttack }, null, this);
            var shockwaveAttack = new ShockwaveAttack(owner, shockwaveAttackObject, player, attackRangeShockwave, agent,null, this);
            //ranged range
            var rangedAttack = new RangedAttack(owner, rangedAttackObject, player, firePoint, rangedRange, meleeRange, agent, new Conditions[] {withinRangeConditionRanged}, this);
            var teleport = new Teleport(owner.transform, player,meleeRange,offsetPlayer, agent, new Conditions[] { withinRangeConditionRanged }, this);
            var randomPattern = new RandomSelector(new Node[] { rangedAttack,rangedAttack,rangedAttack,rangedAttack, rangedAttack, rangedAttack, rangedAttack, rangedAttack,rangedAttack, teleport }, null, this);
            //root
            root = new Selector(new Node[] { shockwaveAttack, meleeSequence, randomPattern }, null, this);
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
