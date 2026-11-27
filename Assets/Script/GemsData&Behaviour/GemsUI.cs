using TMPro;
using UnityEngine;

public class GemsUI : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI GemsCountTMPUI;
    private void Start()
    {
        UpdateGemsCountUI();
    }

    private void OnEnable()
    {
        GemsGlobalData.Instance.ChangeGemsCountEvent += UpdateGemsCountUI;
    }

    private void OnDisable()
    {
        GemsGlobalData.Instance.ChangeGemsCountEvent -= UpdateGemsCountUI;
    }

    private void UpdateGemsCountUI()
    {
        GemsCountTMPUI.text = $"Gems: {GemsGlobalData.Instance.GemsCount}";
    }
}

/* collectable nằm trong collectableobjectcontainer của mỗi floor.
 * khi player nhảy đến floor, quét tất cả gameobject nằm trong collectableobjectcontainer
 * nếu phát hiện collectable thì thực hiện gọi hàm collect của mỗi collectable
 * trước hết sẽ chỉ lưu các vật phẩm ở dạng value(count only)
 */