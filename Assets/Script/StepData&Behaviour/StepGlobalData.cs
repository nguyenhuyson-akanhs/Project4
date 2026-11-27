using System;
using System.Collections.Generic;
using UnityEngine;

public class StepGlobalData : MonoBehaviour
{
    public static StepGlobalData Instance;
    [field: SerializeField] public int StepCount { get; private set; }

    public event Action ChangeStepCountEvent;

    private Queue<int> StepCountQueue = new(); private bool StepCountRequestQueueRunning = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeStepCountRequest(int newStepCount)
    {
        StepCountQueue.Enqueue(newStepCount);
        if (!StepCountRequestQueueRunning)
        {
            StepCountRequestQueueRunning = true;
            _ = StepCountRequestQueue();
        }
    }

    private async Awaitable StepCountRequestQueue()
    {

        int countStep = 0;
        int newCount = StepCount;
        while (StepCountQueue.Count != 0)
        {
            countStep++;
            newCount = StepCountQueue.Dequeue();

            if (countStep >= 100)
            {
                countStep = 0;
                await Awaitable.WaitForSecondsAsync(0.1f, destroyCancellationToken);
            }
        }
        StepCount = newCount;
        ChangeStepCountEvent?.Invoke();

        StepCountRequestQueueRunning = false;

    }
}
