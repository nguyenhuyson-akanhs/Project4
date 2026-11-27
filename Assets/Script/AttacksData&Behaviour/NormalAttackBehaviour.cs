using UnityEngine;

public enum TargetAttack
{
    Enemy,
    Character,
}

public class NormalAttackBehaviour : AttackBehaviour
{
    [SerializeField] private AttackLocalData LocalData;
    public override void Attack(Transform target, TargetAttack typeTarget)
    {
        _ = AttackBehaviour(target, typeTarget);
    }
    public async Awaitable AttackBehaviour(Transform target, TargetAttack typeTarget)
    {
        target = (typeTarget == TargetAttack.Character) ? CharacterGlobalData.Instance.CharacterModel.parent : target;

        await Awaitable.WaitForSecondsAsync(0.1f, destroyCancellationToken);

        float t = 0;
        float percent = 0;

        while (percent != 1)
        {
            percent = t / LocalData.MoveDuration;
            percent = (percent >= 0.99f) ? 1 : percent;

            Vector3 newPos = Vector3.Lerp(LocalData.StartPos, target.transform.position, percent);
            transform.position = newPos;

            await Awaitable.NextFrameAsync(destroyCancellationToken);
            t += Time.deltaTime;
        }

        await Awaitable.NextFrameAsync(destroyCancellationToken);

        if (typeTarget == TargetAttack.Character)
        {
            target.GetComponent<TakeDamageable>().TakeDamage(LocalData.DamageDeal);
        }
        else if(typeTarget == TargetAttack.Enemy)
        {
            Transform enemisContainer = target.GetComponent<FloorLocalData>().EnemisContainer;
            for (int i = enemisContainer.childCount - 1; i >= 0; i--)
            {
                enemisContainer.GetChild(i).GetComponent<TakeDamageable>().TakeDamage(LocalData.DamageDeal);
            }
        }

        await Awaitable.NextFrameAsync(destroyCancellationToken);
        Destroy(gameObject);
    }
}

public class AttackBehaviour : MonoBehaviour
{
    public virtual void Attack(Transform target, TargetAttack typeTarget)
    {

    }
}
