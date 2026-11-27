using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    private void Start()
    {
        CharacterGlobalData.Instance.AttackableArea.gameObject.SetActive(false);
        UpdateAttackableArea();
    }

    private void OnEnable()
    {
        SelectingManager.GameObjectSelected += AttackAction;
        Buttons.AttackMode += AttackModeEnable;
        Buttons.IdleMode += AttackModeDisable;
        SelectingGlobalData.Instance.ChangeAttackableRadiusEvent += UpdateAttackableArea;
    }

    private void OnDisable()
    {
        SelectingManager.GameObjectSelected -= AttackAction;
        Buttons.AttackMode -= AttackModeEnable;
        Buttons.IdleMode -= AttackModeDisable;
        SelectingGlobalData.Instance.ChangeAttackableRadiusEvent -= UpdateAttackableArea;
    }

    private void AttackModeEnable()
    {
        CharacterGlobalData.Instance.AttackableArea.gameObject.SetActive(true);
    }

    private void AttackModeDisable()
    {
        CharacterGlobalData.Instance.AttackableArea.gameObject.SetActive(false);
    }

    private void AttackAction(Transform target)
    {
        if (CharacterGlobalData.Instance.Status != CharacterStatus.Idling || CharacterGlobalData.Instance.Mode != CharacterMode.Attack) return;
        if (target == transform.parent.parent) return;
        if (target.gameObject.TryGetComponent<GenericLocalData>(out var component))
        {
            if (Vector3.Distance(transform.position, component.TruePosition)
                                                > SelectingGlobalData.Instance.AttackableRadius) return;
            Debug.Log("Out Range Attack");
        }

        GameObject newGO = Instantiate(CharacterGlobalData.Instance.ModelTest,transform,false);
        StepGlobalData.Instance.ChangeStepCountRequest(StepGlobalData.Instance.StepCount + newGO.GetComponent<AttackLocalData>().AttackStepCost);
        newGO.GetComponent<AttackBehaviour>().Attack(target,TargetAttack.Enemy);
    }

    private void UpdateAttackableArea()
    {
        CharacterGlobalData.Instance.AttackableArea.localScale = new Vector3(SelectingGlobalData.Instance.AttackableRadius,
                                        SelectingGlobalData.Instance.AttackableRadius,
                                        SelectingGlobalData.Instance.AttackableRadius) * 2f;
    }
}
