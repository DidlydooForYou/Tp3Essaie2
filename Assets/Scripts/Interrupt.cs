using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Interrupt
{
    Conditions[] conditions;
    BehaviorTree BT;
    bool[] conditionState;

    CancellationTokenSource cts;

    public bool Triggered { get; private set; }
    public Interrupt(Conditions[] conditions, BehaviorTree BT)
    {
        this.conditions = conditions;
        this.BT = BT;
        conditionState = new bool[conditions.Length];

        Start();
    }

    async private void CheckConditions(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            for (int index = 0; index < conditions.Length; index++)
            {
                bool current = conditions[index].Evaluate();

                if (current == true && conditionState[index] == false)
                {
                    BT.Interupt();
                }

                conditionState[index] = current;
            }
            await Task.Delay(100);
        }
    }

    private void UpdateState()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            conditionState[i] = conditions[i].Evaluate();
        }
    }

    public void Start()
    {
        Triggered = false;
        cts = new CancellationTokenSource();
        UpdateState();
        CheckConditions(cts.Token);
    }

    public void Stop()
    {
        Triggered = false;
        cts.Cancel();
    }

    public void ResetTrigger()   
    {
        Triggered = false;
    }
}