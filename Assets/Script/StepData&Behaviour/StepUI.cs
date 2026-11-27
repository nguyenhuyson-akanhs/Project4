using TMPro;
using UnityEngine;

public class StepUI : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI StepCountTMPUI;
    private void Start()
    {
        UpdateStepCountUI();
    }

    private void OnEnable()
    {
        StepGlobalData.Instance.ChangeStepCountEvent += UpdateStepCountUI;
    }

    private void OnDisable()
    {
        StepGlobalData.Instance.ChangeStepCountEvent -= UpdateStepCountUI;
    }

    private void UpdateStepCountUI()
    {
        StepCountTMPUI.text = $"Step: {StepGlobalData.Instance.StepCount}";
    }
}
