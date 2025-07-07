using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableItem : MonoBehaviour, IInteractable
{
    public int itemID;
    private InteractableData data;

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
        Debug.Log($"장착된 아이템 : {ItemManager.EquipItem.GetPairID()}");

    }

    public void InteractOnClick() {
        // Monologue
        PlayMonologue();
    }

    public void InteractOnE()
    {
        // Deactive Sound + DeactiveMSG
        PlayDeactiveMSG();

        // PairID 맞는경우 퍼즐해제
    }
}
