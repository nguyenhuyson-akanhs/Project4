using System;
using UnityEngine;

public enum SelectingBoxStats
{
    Busy,
    Ready,
}
public class SelectingGlobalData : MonoBehaviour
{
    public static SelectingGlobalData Instance;

    [field: Header("Global")]
    [field: SerializeField] public Transform PlayerTrs { get; private set; }


    [field: Header("Selecting Options")]
    [field: SerializeField] public float MaxSelectingDistance { get; private set; }
    [field: SerializeField] public LayerMask SelectableLayer { get; private set; }


    [field: Header("Selecting Jump Options")]
    [field: SerializeField] public float JumpableRadius { get; private set; }


    [field: Header("Selecting Attack Options")]
    [field: SerializeField] public float AttackableRadius { get; private set; }


    [field: Header("SelectingBox Options")]
    [field: SerializeField] public Transform MainSelectingBox { get; private set; }
    [field: SerializeField] public Material SelectingBoxMat { get; private set; }
    [field: SerializeField] public Vector3 SelectingBoxDefaultPos { get; private set; }
    [field: HideInInspector] public SelectingBoxStats MainSelectingBoxStats { get; private set; }
    [field: SerializeField][Range(0f, 5f)] public float BoxMoveDuration { get; private set; }
    [field: SerializeField][Range(0f, 255f)] public float MaxAlpha { get; private set; }
    [field: SerializeField][Range(0f, 5f)] public float FacingDuration { get; private set; }

    public event Action ChangeJumpableRadiusEvent;
    public event Action ChangeAttackableRadiusEvent;

    private void Awake()
    {
        Instance = this;
        SelectingBoxDefaultPos = MainSelectingBox.position;
        MainSelectingBoxStats = SelectingBoxStats.Ready;
    }

    private void Intilialize()
    {

    }

    public void ChangeMainSelectingBoxStatsRequest(SelectingBoxStats newStats) => MainSelectingBoxStats = newStats;
    public void ChangeJumpableRadiusRequest(float newJumpableRadius)
    {
        JumpableRadius = newJumpableRadius;
        ChangeJumpableRadiusEvent?.Invoke();
    }

    public void ChangeAttackableRadiusRequest(float newAttackableRadius)
    {
        AttackableRadius = newAttackableRadius;
        ChangeAttackableRadiusEvent?.Invoke();
    }
}
