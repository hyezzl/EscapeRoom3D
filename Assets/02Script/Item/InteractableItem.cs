using cakeslice;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableItem : MonoBehaviour, IInteractable
{
    public int itemID;
    private InteractableData data;

    public int GetItemID() => itemID;
    public int GetPairID() => data.pairID;
    ItemType IActionItem.GetType() => data.type;
    

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetInteractable(itemID);
        if (data == null)
            Debug.Log($"InteractableItem - Failed to Load {itemID} Data");
        if (TryGetComponent<Outline>(out Outline outline)) {
            outline.color = 1;
        }
    }
    public void InteractOnClick() {
        if (ItemManager.CurrentItem == null) return;
        Debug.Log($"현재 선택된 대상 : {ItemManager.CurrentItem}");
        // Monologue
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, true));
    }

    public void InteractOnE()
    {
        if (ItemManager.EquipItem != null &&
            data.pairID == ItemManager.EquipItem.pairID)
        {
            // 장착한 아이템과 Interactable이 짝일 때
            Debug.Log("@@@@@@@@@@@@@@@@@@@@퍼즐 해결!@@@@@@@@@@@@@@@@@@@@");
            
            // 특정 애니메이션

            EventBus.Instance.Publish<PuzzleEvents.DoInteract>(new PuzzleEvents.DoInteract(data.pairID, this.gameObject));
        }
        else { 
            // Deactive Sound + DeactiveMSG
            EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, false));
        }
    }
}
