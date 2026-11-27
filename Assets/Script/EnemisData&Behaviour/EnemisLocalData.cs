using System;
using UnityEngine;

public class EnemisLocalData : MonoBehaviour, TakeDamageable
{
    public event Action EnemyDeath;

    [field: Header("Enemy Stats Options")]
    [field: SerializeField] public float MaxHP { get; private set; }

    [field: Header("Enemy Attack Options")] // first only one skill
    [field: SerializeField] public int AttackableRadius {  get; private set; }
    [field: SerializeField] public int AttackStepCost {  get; private set; }
    [field: HideInInspector] public int AttackStepPaid { get; private set; }
    [field: SerializeField] public Transform AttackPoint { get; private set; }
    [field: SerializeField] public GameObject ModelTest { get; private set; }

    [field: HideInInspector] public float CurrentHP {  get; private set; }

    private void Awake()
    {
        CurrentHP = MaxHP;
        AttackStepPaid = 0;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
        CurrentHP = (CurrentHP < 0) ? 0 : CurrentHP;
        if (CurrentHP == 0)
        {
            Destroy(gameObject);
            EnemyDeath?.Invoke();
        }
    }

    public void ChangeAttackStepPaidRequest(int newAttackStepPaid) => AttackStepPaid = newAttackStepPaid;
}

public interface TakeDamageable
{
    public void TakeDamage(float damage);
}
