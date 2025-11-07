using UnityEngine;

public class HasVision : Conditions
{
    GameObject target;
    Transform self;
    float visionAngle;
    float distanceVision;

    public HasVision(Transform self, GameObject target, float visionAngle, float distanceVision, bool reverseCondition = false)
    {
        this.self = self;
        this.target = target;
        this.visionAngle = visionAngle;
        this.reverseCondition = reverseCondition;
        this.distanceVision = distanceVision;
    }

    public override bool Evaluate()
    {
        Vector3 direction = target.transform.position - self.position;
        direction.Normalize();

        float angleToTarget = Vector3.Angle(self.forward, direction);
        float distance = Vector3.Distance(target.transform.position, self.position);

        if (angleToTarget > visionAngle || distance > distanceVision)
        {
            return CheckForReverse(false);
        }

        if (Physics.Raycast(self.transform.position, direction, out RaycastHit hit))
        {
            if (hit.collider.gameObject != target)
            {
                return CheckForReverse(false);
            }
        }
        return CheckForReverse(true);
    }
}