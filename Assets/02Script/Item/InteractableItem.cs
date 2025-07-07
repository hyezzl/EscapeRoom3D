using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }


    public void PlayMonologue()
    {
        Debug.Log($"독백 재생 : {data.monologue}");
    }

    public void InteractOnClick() { }

    public void InteractOnE()
    {
        // Deactive Sound + DeactiveMSG

        // PairID 맞는경우 퍼즐해제
    }
}
