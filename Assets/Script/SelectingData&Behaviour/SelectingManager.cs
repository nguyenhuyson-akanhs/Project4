using System;
using UnityEngine;

public class SelectingManager : MonoBehaviour
{
    public static event Action<Transform> GameObjectSelected;

    private void OnEnable()
    {
        Buttons.IdleMode += MoveBackToDefaultPos;
    }
    private void OnDisable()
    {
        Buttons.IdleMode -= MoveBackToDefaultPos;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && SelectingGlobalData.Instance.MainSelectingBoxStats == SelectingBoxStats.Ready)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, SelectingGlobalData.Instance.MaxSelectingDistance, SelectingGlobalData.Instance.SelectableLayer))
            {
                GameObjectSelected?.Invoke(hit.transform);

                _ = SelectBoxMove(hit.transform);
            }
            else
            {
                _ = SelectBoxMoveBackToDefaultPos();
            }
        }
    }

    private async Awaitable SelectBoxMove(Transform objSelected)
    {
        SelectingGlobalData.Instance.ChangeMainSelectingBoxStatsRequest(SelectingBoxStats.Busy);

        float currentTime = 0f;
        float percent = 0f;

        Vector3 startPos = SelectingGlobalData.Instance.MainSelectingBox.position;

        while (true)
        {
            percent = Mathf.Clamp01(currentTime / SelectingGlobalData.Instance.BoxMoveDuration);
            SelectingGlobalData.Instance.MainSelectingBox.position = Vector3.Lerp(startPos, objSelected.position, percent);

            await Awaitable.NextFrameAsync(destroyCancellationToken);

            if (percent == 1f) break;
            currentTime += Time.deltaTime;
        }

        SelectingGlobalData.Instance.MainSelectingBox.parent = objSelected;
        SelectingGlobalData.Instance.ChangeMainSelectingBoxStatsRequest(SelectingBoxStats.Ready);
    }

    private void MoveBackToDefaultPos()
    {
        if (SelectingGlobalData.Instance.MainSelectingBoxStats == SelectingBoxStats.Ready) _ = SelectBoxMoveBackToDefaultPos();
    }
    private async Awaitable SelectBoxMoveBackToDefaultPos()
    {
        SelectingGlobalData.Instance.ChangeMainSelectingBoxStatsRequest(SelectingBoxStats.Busy);

        float currentTime = 0f;
        float percent = 0f;

        Vector3 startPos = SelectingGlobalData.Instance.MainSelectingBox.position;

        while (true)
        {
            percent = Mathf.Clamp01(currentTime / SelectingGlobalData.Instance.BoxMoveDuration);
            SelectingGlobalData.Instance.MainSelectingBox.position = Vector3.Lerp(startPos, SelectingGlobalData.Instance.SelectingBoxDefaultPos, percent);

            await Awaitable.NextFrameAsync(destroyCancellationToken);

            if (percent == 1f) break;
            currentTime += Time.deltaTime;
        }

        SelectingGlobalData.Instance.MainSelectingBox.parent = transform;
        SelectingGlobalData.Instance.ChangeMainSelectingBoxStatsRequest(SelectingBoxStats.Ready);
    }
}
