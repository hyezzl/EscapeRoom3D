using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour, IPickable
{
    public int itemID;
    private ObjectData data;


    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetData(itemID);
        if (data == null)
            Debug.Log($"Failed to Load {itemID} Data");

        if (TryGetComponent<Rigidbody>(out Rigidbody rig)){
            rig.useGravity = false;
        }
    }

    public void OnpickUp()
    {
        Debug.Log("줍기행동");
        Destroy(gameObject);
    }

    ItemType IActionItem.GetType()
    {
        return data.type;
    }
}
