using UnityEngine;

public class CharacterJumpController : MonoBehaviour
{
    private void Start()
    {
        CharacterGlobalData.Instance.JumpableArea.gameObject.SetActive(false);
        UpdateJumpableArea();
    }

    private void OnEnable()
    {
        SelectingManager.GameObjectSelected += JumpAction;
        Buttons.JumpMode += JumpModeEnable;
        Buttons.IdleMode += JumpModeDisable;
        SelectingGlobalData.Instance.ChangeJumpableRadiusEvent += UpdateJumpableArea;
        //SelectTesting.GameObjectSelected += MoveAction;
    }

    private void OnDisable()
    {
        SelectingManager.GameObjectSelected -= JumpAction;
        Buttons.JumpMode -= JumpModeEnable;
        Buttons.IdleMode -= JumpModeDisable;
        SelectingGlobalData.Instance.ChangeJumpableRadiusEvent -= UpdateJumpableArea;
        //SelectTesting.GameObjectSelected -= MoveAction;
    }

    private void JumpModeEnable()
    {
        CharacterGlobalData.Instance.JumpableArea.gameObject.SetActive(true);
    }
    private void JumpModeDisable()
    {
        CharacterGlobalData.Instance.JumpableArea.gameObject.SetActive(false);
    }
    private void JumpAction(Transform nextFloor)
    {
        if (CharacterGlobalData.Instance.Status != CharacterStatus.Idling || CharacterGlobalData.Instance.Mode != CharacterMode.Jump) return;
        if (nextFloor == transform.parent.parent) return;
        if (nextFloor.gameObject.TryGetComponent<GenericLocalData>(out var component))
        {
            if (Vector3.Distance(transform.position, component.TruePosition)
                                                > SelectingGlobalData.Instance.JumpableRadius) return;
            Debug.Log("Out Range Jump");
        }

        StepGlobalData.Instance.ChangeStepCountRequest(StepGlobalData.Instance.StepCount + CharacterGlobalData.Instance.JumpStepCost);
        _ = JumpToNextFloor(nextFloor);
    }
    private void UpdateJumpableArea()
    {
        CharacterGlobalData.Instance.JumpableArea.localScale = new Vector3(SelectingGlobalData.Instance.JumpableRadius,
                                        SelectingGlobalData.Instance.JumpableRadius,
                                        SelectingGlobalData.Instance.JumpableRadius) * 2f;
    }

    private async Awaitable JumpToNextFloor(Transform nextFloor)
    {
        transform.parent = null;

        CharacterGlobalData.Instance.ChangeCharacterStatusRequest(CharacterStatus.Jumping);

        Vector3 startPos = transform.position;

        float duration = 0;
        while (true)
        {
            float percent = duration / CharacterGlobalData.Instance.JumpDuration;
            percent = (percent >= 0.99f) ? 1 : percent;

            transform.position = SimpleParabolJump(startPos,
                                                   nextFloor.position,
                                                   CharacterGlobalData.Instance.JumpHigh,
                                                   percent);

            if (percent == 1) break;

            duration += Time.deltaTime;
            await Awaitable.NextFrameAsync(destroyCancellationToken);
        }

        transform.position = nextFloor.position;
        transform.parent = nextFloor.GetComponent<FloorLocalData>().CharacterContainer;
        await Awaitable.WaitForSecondsAsync(0.5f, destroyCancellationToken);

        CharacterGlobalData.Instance.CharacterCollectItems.CollectCollectableItems();
        CharacterGlobalData.Instance.ChangeCharacterStatusRequest(CharacterStatus.Idling);
    }

    private Vector3 SimpleParabolJump(Vector3 startPos, Vector3 endPos, float high, float t)
    {
        float yTop = Mathf.Max(startPos.y, endPos.y)
                   + high;
        float const4h = Mathf.Pow((Mathf.Sqrt(yTop - startPos.y)
                      + Mathf.Sqrt(yTop - endPos.y)), 2);

        Vector3 newPos = new Vector3
            (
                startPos.x + (endPos.x - startPos.x) * t,
                startPos.y + (endPos.y - startPos.y) * t + const4h * t * (1 - t),
                startPos.z + (endPos.z - startPos.z) * t
            );
        return newPos;
    }
}
