using UnityEngine;

public class EnemisBehaviour : MonoBehaviour
{
    [field: SerializeField] public EnemisLocalData LocalData;

    private int CurrentStepCount;
    private void Start()
    {
        CurrentStepCount = StepGlobalData.Instance.StepCount;
    }

    private void OnEnable()
    {
        StepGlobalData.Instance.ChangeStepCountEvent += AttackAction;
    }
    private void OnDisable()
    {
        StepGlobalData.Instance.ChangeStepCountEvent -= AttackAction;
    }

    private void AttackAction()
    {
        Transform target = CharacterGlobalData.Instance.CharacterModel.parent;

        if (Vector3.Distance(target.position, transform.position) > LocalData.AttackableRadius) return;
        Debug.Log(1);

        int deltaStepCount = StepGlobalData.Instance.StepCount - CurrentStepCount;
        deltaStepCount = (deltaStepCount < 0) ? 0 : deltaStepCount;
        CurrentStepCount = StepGlobalData.Instance.StepCount;

        int attackCount = (deltaStepCount + LocalData.AttackStepPaid) / LocalData.AttackStepCost;
        LocalData.ChangeAttackStepPaidRequest((deltaStepCount + LocalData.AttackStepPaid) % LocalData.AttackStepCost);
        Debug.Log(2);
        // do attack...
        _ = AttackTarget(target, attackCount);
    }

    private async Awaitable AttackTarget(Transform target, int attackCount)
    {
        Debug.Log(3);
        while (attackCount-- != 0)
        {
            //if (target.parent == null
            //    || target.parent.parent == null
            //    || target.parent.parent.parent == null) await Awaitable.WaitForSecondsAsync(0.5f);

            //if (target.parent == null
            //    || target.parent.parent == null
            //    || target.parent.parent.parent == null)
            //{
            //    Debug.LogWarning("Get FK here");
            //    break;
            //}

            GameObject newGO = Instantiate(LocalData.ModelTest, transform, false);
            newGO.transform.localScale = new Vector3(newGO.transform.localScale.x / transform.localScale.x,
                                                      newGO.transform.localScale.y / transform.localScale.y,
                                                      newGO.transform.localScale.z / transform.localScale.z);
            newGO.GetComponent<AttackBehaviour>().Attack(target, TargetAttack.Character);

            await Awaitable.WaitForSecondsAsync(0.2f);
        }
        Debug.Log(4);
    }
}


/*
 * + enemybehaviour:
 * character get in attack radius + attackstepPaid = attackcost -> attack. first demo with one type attack
 * after use that attack, attackstepPaid = 0 
 * attackstepPaid increase when stepcount(global) increase
 * 
 * + change bullet(cube) script ...
 * 
 *
 * character in enemy attack radius -> attackstepPaid up when stepcount up
 * character out enemy attack radius -> attackstePaid stop increase
 * 
 * attackstepPaid down to 0 -> cast enemy
 */
