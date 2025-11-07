using UnityEngine;

abstract public class BehaviorTree : MonoBehaviour
{
    protected Node root;

    public Node activeNode;

    abstract protected void InitializeTree();

    void Start()
    {
        InitializeTree();
        EvaluateTree();
    }


    void Update()
    {
        if (activeNode != null)
            activeNode.Tick(Time.deltaTime);
    }
    public void EvaluateTree()
    {
        root.ExecuteAction();
    }
    public void Interupt()
    {
        activeNode.Interrupt();
        EvaluateTree();
    }
}