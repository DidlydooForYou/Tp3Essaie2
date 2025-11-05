using UnityEngine;

public enum RangeMode { Melee, Ranged, Far }

public class WithinRange : Conditions
{
    GameObject target;
    Transform self;
    float rangeMelee;
    float rangeRanged;
    float distanceBetweenTarget;

    public RangeMode CurrentMode { get; private set; }

    public WithinRange(Transform self, GameObject target, float rangeMelee, float rangeRanged, bool reverseCondition = false)
    {
        this.self = self;
        this.target = target;
        this.rangeMelee = rangeMelee;
        this.rangeRanged = rangeRanged;
        this.reverseCondition = reverseCondition;
    }

    public override bool Evaluate()
    {
        distanceBetweenTarget = (target.transform.position - self.position).magnitude;

        if(distanceBetweenTarget <= rangeMelee)
        {
            Debug.Log("Target is within melee range");
            CurrentMode = RangeMode.Melee;
        }
        else if (distanceBetweenTarget <= rangeRanged && distanceBetweenTarget > rangeMelee)
        {
            Debug.Log("Target is within ranged range");
            CurrentMode = RangeMode.Ranged;
        }
        else if(distanceBetweenTarget > rangeRanged)
        {
            Debug.Log("Target needs to dash");
            CurrentMode = RangeMode.Far;

        }
        return CheckForReverse(true);
    }
}
