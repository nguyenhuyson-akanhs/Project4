using System;
using UnityEngine;

public enum OnObjectEffect
{
    On,
    Off,
}

[Serializable]
public struct RangeDuration
{
    public float MinDuration;
    public float MaxDuration;
    public float MaxBreakDuration;
}

public class FloorGlobalData : MonoBehaviour
{
    public static FloorGlobalData Instance;
    [field: SerializeField] public float MaxRange { get; private set; }
    [field: SerializeField] public RangeDuration Duration { get; private set; }

    private void Awake() => Instance = this;
}
