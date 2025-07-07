using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class PickableItem : MonoBehaviour, IPickable
{
    public int itemID;
    private PickableData data;
    private PlayerSight sight;

    ItemType IActionItem.GetType() => data.type;
    public int GetPairID() => data.pairID; ////////////

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetPickable(itemID);
        if (data == null)
            Debug.Log($"PickableItem - Failed to Load {itemID} Data");

        if (TryGetComponent<Rigidbody>(out Rigidbody rig)){
            rig.useGravity = false;
        }
        if (TryGetComponent<Outline>(out Outline outline)) {
            outline.color = 1;
        }
    }

    public void OnpickUp()
    {
        Debug.Log("줍기행동");

        ItemManager.CurrentItem = null;
        //리스트 비워주기 !!!!!
        // 이벤트 구독(아이템습득) 1프레임 쉰 후 한번더 오버랩
        Destroy(gameObject);
    }

    public void InteractOnClick()
    {
        OnpickUp();
    }

    public void InteractOnE() { Debug.Log("E 기능없음"); }
}
