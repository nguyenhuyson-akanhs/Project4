using TMPro;
using UnityEngine;

public class CharacterHPUI : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI CharacterHPTMPUI;

    private void OnEnable()
    {
        CharacterGlobalData.Instance.AnnounceChangedCharacterCurrentHPEvent += UpdateCharacterHPUI;
    }

    private void OnDisable()
    {
        CharacterGlobalData.Instance.AnnounceChangedCharacterCurrentHPEvent -= UpdateCharacterHPUI;
    }

    private void UpdateCharacterHPUI(float currentHP)
    {
        CharacterHPTMPUI.text = $"HP: {currentHP}";
    }
}

/* collectable nằm trong collectableobjectcontainer của mỗi floor.
 * khi player nhảy đến floor, quét tất cả gameobject nằm trong collectableobjectcontainer
 * nếu phát hiện collectable thì thực hiện gọi hàm collect của mỗi collectable
 * trước hết sẽ chỉ lưu các vật phẩm ở dạng value(count only)
 */