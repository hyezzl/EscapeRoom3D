using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableItem : MonoBehaviour, IInteractable
{
    public int itemID;
    private InteractableData data;

    public int GetItemID() => itemID;
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


    public void PlayMonologue()
    {
        Debug.Log($"독백 재생 : {data.monologue}");
    }

    public void PlayDeactiveMSG() {
        Debug.Log($"Deactive : {data.deactiveMSG}");
        Debug.Log($"바라보고 있는 오브젝트 : {data.pairID}");
        //Debug.Log($"장착된 아이템 : {ItemManager.EquipItem.GetPairID()}");

    }

    public void InteractOnClick() {
        // Monologue
        PlayMonologue();
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
        }
        else { 
            // Deactive Sound + DeactiveMSG
            PlayDeactiveMSG();
            EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, false));
        }



        // PairID 맞는경우 퍼즐해제
    }
}
