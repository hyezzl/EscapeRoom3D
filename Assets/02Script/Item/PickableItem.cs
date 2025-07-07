using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour, IPickable
{
    public int itemID;
    private PickableData data;

    ItemType IActionItem.GetType() => data.type;


    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetPickable(itemID);
        if (data == null)
            Debug.Log($"PickableItem - Failed to Load {itemID} Data");

        if (TryGetComponent<Rigidbody>(out Rigidbody rig)){
            rig.useGravity = false;
        }
    }

    public void OnpickUp()
    {
        Debug.Log("줍기행동");
        Destroy(gameObject);
    }

    public void InteractOnClick()
    {
        // Pickup
    }

    public void InteractOnE() { }
}
