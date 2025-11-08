using UnityEngine;

public enum RangeMode { Melee, Ranged, Far }

public class WithinRange : Conditions
{
    Transform target;
    Transform self;
    float rangeMelee;
    float distanceBetweenTarget;

    public RangeMode CurrentMode { get; private set; }
    private RangeMode wantedMode;

    public WithinRange(Transform self, Transform target, float rangeMelee, RangeMode wantedMode, bool reverseCondition = false)
    {
        this.self = self;
        this.target = target;
        this.rangeMelee = rangeMelee;
        this.wantedMode = wantedMode;
        this.reverseCondition = reverseCondition;
    }

    public override bool Evaluate()
    {
        distanceBetweenTarget = (target.position - self.position).magnitude;

        if (distanceBetweenTarget <= rangeMelee)
        {
            CurrentMode = RangeMode.Melee;
            wantedMode = RangeMode.Melee;
        }
        else if (distanceBetweenTarget > rangeMelee)
        {
            CurrentMode = RangeMode.Ranged;
            wantedMode = RangeMode.Ranged;
        }

        bool inWantedRange = (CurrentMode == wantedMode);
        return CheckForReverse(inWantedRange);
    }
}
