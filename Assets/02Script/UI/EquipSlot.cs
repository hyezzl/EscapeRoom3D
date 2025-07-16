using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Sprite defaultIcon;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.EquipItem>(ShowIcon);
        EventBus.Instance.Subscribe<GameEvents.UnequipItem>(DeleteIcon);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.EquipItem>(ShowIcon);
        EventBus.Instance.Unsubscribe<GameEvents.UnequipItem>(DeleteIcon);
    }

    private void ShowIcon(GameEvents.EquipItem evt) {
        // 아이콘 표시
        PickableData data = ItemDatabaseManager.Instance.GetPickable(evt.itemID);
        if (data != null)
        {
            icon.sprite = data.icon;
        }
        else {
            Debug.Log("EquipSlot - Failed to Load EquipItem's Icon");
        }
    }

    private void DeleteIcon(GameEvents.UnequipItem evt) {
        icon.sprite = defaultIcon;
    }
}
