using UnityEngine;

public enum RangeMode { Melee, Ranged, Far }

public class WithinRange : Conditions
{
    Transform target;
    Transform self;
    float rangeMelee;
    float rangeRanged;
    float distanceBetweenTarget;

    public RangeMode CurrentMode { get; private set; }
    private RangeMode wantedMode;

    public WithinRange(Transform self, Transform target, float rangeMelee, float rangeRanged, RangeMode wantedMode, bool reverseCondition = false)
    {
        this.self = self;
        this.target = target;
        this.rangeMelee = rangeMelee;
        this.rangeRanged = rangeRanged;
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
        else if (distanceBetweenTarget <= rangeRanged)
        {
            CurrentMode = RangeMode.Ranged;
            wantedMode = RangeMode.Ranged;
        }
        else
        {
            CurrentMode = RangeMode.Far;
            wantedMode = RangeMode.Far;
        }

        bool inWantedRange = (CurrentMode == wantedMode);
        Debug.Log($"WithinRange - Distance: {distanceBetweenTarget:F1}, Current: {CurrentMode}, Wanted: {wantedMode}, Match: {inWantedRange}");
        return CheckForReverse(inWantedRange);
    }
}
