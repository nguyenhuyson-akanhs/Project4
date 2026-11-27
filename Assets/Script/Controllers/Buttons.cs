using System;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public static event Action IdleMode;
    public static event Action JumpMode;
    public static event Action AttackMode;
    public void JumpModeActive()
    {
        if(CharacterGlobalData.Instance.Mode == CharacterMode.Jump)
        {
            IdleMode?.Invoke();
            CharacterGlobalData.Instance.ChangeCharacterModeRequest(CharacterMode.Idle);
            return;
        }

        IdleMode?.Invoke();
        JumpMode?.Invoke();
        CharacterGlobalData.Instance.ChangeCharacterModeRequest(CharacterMode.Jump);
    }

    public void AttackModeActive()
    {
        if (CharacterGlobalData.Instance.Mode == CharacterMode.Attack)
        {
            IdleMode?.Invoke();
            CharacterGlobalData.Instance.ChangeCharacterModeRequest(CharacterMode.Idle);
            return;
        }

        IdleMode?.Invoke();
        AttackMode?.Invoke();
        CharacterGlobalData.Instance.ChangeCharacterModeRequest(CharacterMode.Attack);
    }
}
