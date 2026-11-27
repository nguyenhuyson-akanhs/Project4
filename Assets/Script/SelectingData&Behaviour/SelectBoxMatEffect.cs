using UnityEngine;

public class SelectBoxMatEffect : MonoBehaviour
{
    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private OnObjectEffect ObjEffect;
    private void Awake()
    {
        ObjEffect = OnObjectEffect.Off;
    }

    private void Update()
    {
        if (ObjEffect == OnObjectEffect.Off) _ = ChangeAlpha();
    }

    private async Awaitable ChangeAlpha()
    {
        ObjEffect = OnObjectEffect.On;

        float currentTime = 0f;
        float percent = 0f;

        // ALPHA_UP
        while (true)
        {
            percent = currentTime / SelectingGlobalData.Instance.FacingDuration;
            percent = (percent >= 0.99f) ? 1f : percent;

            Color color = SelectingGlobalData.Instance.SelectingBoxMat.GetColor(BaseColorID);
            color.a = Mathf.Lerp(0, SelectingGlobalData.Instance.MaxAlpha, percent) / 255f;
            SelectingGlobalData.Instance.SelectingBoxMat.SetColor(BaseColorID, color);

            await Awaitable.NextFrameAsync(destroyCancellationToken);

            if (percent == 1f) break;
            currentTime += Time.deltaTime;
        }

        currentTime = 0f;
        percent = 0f;

        // ALPHA_DOWN
        while (true)
        {
            percent = currentTime / SelectingGlobalData.Instance.FacingDuration;
            percent = (percent >= 0.99f) ? 1f : percent;

            Color color = SelectingGlobalData.Instance.SelectingBoxMat.GetColor(BaseColorID);
            color.a = Mathf.Lerp(SelectingGlobalData.Instance.MaxAlpha, 0, percent) / 255f;
            SelectingGlobalData.Instance.SelectingBoxMat.SetColor(BaseColorID, color);

            await Awaitable.NextFrameAsync(destroyCancellationToken);

            if (percent == 1f) break;
            currentTime += Time.deltaTime;
        }

        ObjEffect = OnObjectEffect.Off;
    }
}
