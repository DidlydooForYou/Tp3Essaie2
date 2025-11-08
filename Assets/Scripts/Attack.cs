using NUnit.Framework;
using UnityEngine;

public class Attack : Node
{
    Transform self;
    string targetTag;
    Vector3 range;
    Vector3 originOffset;
    Vector3 direction;
    private float attackDuration = 1f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    public Attack(Vector3 direction, Vector3 originOffset, Vector3 range, string targetTag, Transform self, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.self = self;
        this.targetTag = targetTag;
        this.range = range;
        this.originOffset = originOffset;
        this.direction = direction;
    }

    public override void ExecuteAction()
    {
        isAttacking = true;
        attackTimer = attackDuration;
        RaycastHit[] hits = Physics.BoxCastAll(self.position + originOffset, range / 2, self.forward);
        bool success = false;
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag == targetTag)
                {
                    success = true;
                    hit.collider.gameObject.GetComponent<SanityUpdate>().LoseSanity(10);
                    FinishAction(success);
                }
            }
        }

        base.ExecuteAction();
    }
    public override void Tick(float deltaTime)
    {

        if (!isAttacking)
        {
            FinishAction(false);
            return;
        }
        if (attackTimer <= 0f)
        {
            isAttacking = false;
            FinishAction(true);
        }
    }

    public override void FinishAction(bool result)
    {
        base.FinishAction(result);
    }
}
