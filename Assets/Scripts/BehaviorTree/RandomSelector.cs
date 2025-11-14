using UnityEngine;

public class RandomSelector : Node
{
    Node[] children;
    int randomIndex = 0;

    public RandomSelector(Node[] Children, Conditions[] condition, BehaviorTree BT) : base(condition, BT)
    {
        this.children = Children;
        foreach (Node child in children)
        {
            child.SetParent(this);
        }
    }
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        if(children.Length == 0)
        {
            FinishAction(false);
            return;
        }
        randomIndex = Random.Range(0, children.Length);
        children[randomIndex].ExecuteAction();
    }
    //public override void FinishAction(bool result)
    //{
    //    base.FinishAction(result);
    //}
}
