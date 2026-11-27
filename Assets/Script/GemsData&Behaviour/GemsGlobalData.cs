using System;
using System.Collections.Generic;
using UnityEngine;

public class GemsGlobalData : MonoBehaviour
{
    public static GemsGlobalData Instance;
    [field: SerializeField] public int GemsCount { get; private set; }

    public event Action ChangeGemsCountEvent;

    private Queue<int> GemsCountQueue = new(); private bool GemsCountRequestQueueRunning = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeGemsCountRequest(int newGemsCount)
    {
        GemsCountQueue.Enqueue(newGemsCount);
        if (!GemsCountRequestQueueRunning)
        {
            GemsCountRequestQueueRunning = true;
            _ = GemsCountRequestQueue();
        }
    }

    private async Awaitable GemsCountRequestQueue()
    {

        int countStep = 0;
        int newCount = GemsCount;
        while (GemsCountQueue.Count != 0)
        {
            countStep++;
            newCount = GemsCountQueue.Dequeue();

            if (countStep >= 100)
            {
                countStep = 0;
                await Awaitable.WaitForSecondsAsync(0.1f,destroyCancellationToken);
            }
        }
        GemsCount = newCount;
        ChangeGemsCountEvent?.Invoke();

        GemsCountRequestQueueRunning = false;

    }
}
