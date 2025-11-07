using UnityEngine;

public class InRange : Conditions
{
    Transform self;
    GameObject target;
    float range;

    public InRange(Transform self, GameObject target, int range, bool reverseConditions = false)
    {
        this.self = self;
        this.target = target;
        this.range = range;
        this.reverseCondition = reverseConditions;
    }

    public override bool Evaluate()
    {
        float distance = Vector3.Distance(self.position, target.transform.position);

        bool inRange = distance <= range;

        return CheckForReverse(inRange);
    }

}
