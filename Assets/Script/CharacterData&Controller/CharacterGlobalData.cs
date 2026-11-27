using System;
using UnityEngine;
public enum CharacterStatus
{
    Idling,
    Jumping,
    Attacking,
}

public enum CharacterMode
{
    Idle,
    Jump,
    Attack,
}

public class CharacterGlobalData : MonoBehaviour
{
    public static CharacterGlobalData Instance;
    [field: HideInInspector] public CharacterStatus Status { get; private set; }
    [field: HideInInspector] public CharacterMode Mode { get; private set; }


    [field: Header("Global")]
    [field: SerializeField] public Transform CharacterModel { get; private set; }


    [field: Header("Character Collect Items Options")]
    [field: SerializeField] public CharacterCollectCollectableItems CharacterCollectItems { get; private set; }


    [field: Header("Character Stats Options")]
    [field: SerializeField] public float MaxHP {  get; private set; }


    [field: Header("Character Jump Options")]
    [field: SerializeField] public CharacterJumpController CharcaterJump { get; private set; }
    [field: SerializeField] public float JumpDuration { get; private set; }
    [field: SerializeField] public float JumpHigh { get; private set; }
    [field: SerializeField] public Transform JumpableArea { get; private set; }
    [field: SerializeField] public int JumpStepCost { get; private set; }


    [field: Header("Character Attack Options")]
    [field: SerializeField] public CharacterAttackController CharcaterAttack { get; private set; }
    [field: SerializeField] public Transform AttackPoint {  get; private set; }
    [field: SerializeField] public Transform AttackableArea { get; private set; }
    [field: SerializeField] public GameObject ModelTest { get; private set; }

    public event Action AnnounceCharacterDeathEvent;
    public event Action<float> AnnounceChangedCharacterCurrentHPEvent;
    

    private void Awake()
    {
        Instance = this;

        Status = CharacterStatus.Idling;
        Mode = CharacterMode.Idle;
    }

    public void ChangeCharacterStatusRequest(CharacterStatus newStatus) => Status = newStatus;
    public void ChangeCharacterModeRequest(CharacterMode newMode) => Mode = newMode;
    public void AnnounceCharacterDeathMethod() => AnnounceCharacterDeathEvent?.Invoke();
    public void AnnounceChangedCharacterCurrentHPMethod(float currentHP) => AnnounceChangedCharacterCurrentHPEvent?.Invoke(currentHP);
}
