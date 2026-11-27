using UnityEngine;

public class FloorEffect_1 : MonoBehaviour
{
    [SerializeField] private FloorLocalData LocalData;
    private OnObjectEffect ObjectEffect;
    
    private void Awake()
    {
        ObjectEffect = OnObjectEffect.Off;
    }

    private void Update()
    {
        if (ObjectEffect == OnObjectEffect.Off) _ = UpDownEffect();
    }

    private async Awaitable UpDownEffect()
    {
        ObjectEffect = OnObjectEffect.On;

        float rad1 = UnityEngine.Random.Range(0, Mathf.PI);
        float rad2 = UnityEngine.Random.Range(0, Mathf.PI * 2 - 0.00001f);
        float range = UnityEngine.Random.Range(0, FloorGlobalData.Instance.MaxRange);

        Vector3 targetPosition = new Vector3
        (
            LocalData.TruePosition.x + range * Mathf.Sin(rad1) * Mathf.Cos(rad2),
            LocalData.TruePosition.y + range * Mathf.Sin(rad1) * Mathf.Sin(rad2),
            LocalData.TruePosition.z + range * Mathf.Cos(rad1)
        );

        float duration = 0f;
        float currentTime = 0f;
        float breakDuration = 0f;
        Vector3 currentPosition = LocalData.TruePosition;

        // GO
        duration = UnityEngine.Random.Range(FloorGlobalData.Instance.Duration.MinDuration,
                                            FloorGlobalData.Instance.Duration.MaxDuration);
        breakDuration = UnityEngine.Random.Range(0, FloorGlobalData.Instance.Duration.MaxBreakDuration);
        currentTime = 0f;

        while(true)
        {
            float percent = currentTime / duration;
            percent = (percent > 0.99f) ? 1 : percent;
            currentPosition = Vector3.Lerp(LocalData.TruePosition, targetPosition, percent);
            LocalData.transform.position = currentPosition;
            if (percent == 1) break;

            currentTime += Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        await Awaitable.WaitForSecondsAsync(breakDuration);

        //BACK
        duration = UnityEngine.Random.Range(FloorGlobalData.Instance.Duration.MinDuration,
                                            FloorGlobalData.Instance.Duration.MaxDuration);
        breakDuration = UnityEngine.Random.Range(0, FloorGlobalData.Instance.Duration.MaxBreakDuration);
        currentTime = 0f;

        while (true)
        {
            float percent = currentTime / duration;
            percent = (percent > 0.99f) ? 1 : percent;
            currentPosition = Vector3.Lerp(targetPosition, LocalData.TruePosition, percent);
            LocalData.transform.position = currentPosition;
            if (percent == 1) break;

            currentTime += Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        await Awaitable.WaitForSecondsAsync(breakDuration);

        ObjectEffect = OnObjectEffect.Off;
    }
}
