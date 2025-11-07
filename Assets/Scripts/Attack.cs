using NUnit.Framework;
using UnityEngine;

public class Attack : Node
{
    Transform self;
    string targetTag;
    Vector3 range;
    Vector3 originOffset;
    Vector3 direction;
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
}
