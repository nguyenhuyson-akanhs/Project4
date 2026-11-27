using UnityEngine;

public class CharacterStatsController : MonoBehaviour, TakeDamageable
{
    private float CurrentHP;

    private void Start()
    {
        CurrentHP = CharacterGlobalData.Instance.MaxHP;
        TakeDamage(0);
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
        CurrentHP = Mathf.Clamp(CurrentHP,0,CharacterGlobalData.Instance.MaxHP);

        if (CurrentHP == 0) CharacterGlobalData.Instance.AnnounceCharacterDeathMethod();

        CharacterGlobalData.Instance.AnnounceChangedCharacterCurrentHPMethod(CurrentHP);
    }
}
