using UnityEngine;

public class InRange : Conditions
{
    Transform self;
    GameObject target;
    float range = 10;

    public InRange(Transform self, GameObject target, bool reverseConditions = false)
    {
        this.self = self;
        this.target = target;
        this.reverseCondition = reverseConditions;
    }

    public override bool Evaluate()
    {
        float distance = Vector3.Distance(target.transform.position, self.position);

        if(distance > range)
            return CheckForReverse(false);

        return CheckForReverse(true);
    }

}
