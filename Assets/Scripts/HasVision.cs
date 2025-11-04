using UnityEngine;

public class HasVision : Conditions
{
    GameObject target;
    Transform self;
    float visionAngle;

    public HasVision(Transform self, GameObject target, float visionAngle, bool reverseCondition = false)
    {
        this.self = self;
        this.target = target;
        this.visionAngle = visionAngle;
        this.reverseCondition = reverseCondition;
    }

    public override bool Evaluate()
    {
        Vector3 direction = target.transform.position - self.position;
        direction.Normalize();

        float angleToTarget = Vector3.Angle(self.forward, direction);

        if (angleToTarget > visionAngle)
            return CheckForReverse(false);

        if (Physics.Raycast(self.position, direction, out RaycastHit hit))
        {
            if (hit.collider.gameObject != target)
            {
                return CheckForReverse(false);
            }
        }
        return CheckForReverse(true);
    }
}