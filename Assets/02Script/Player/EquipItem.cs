using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private Transform leftDummy;
    private AsyncOperationHandle<GameObject> handle;
    private GameObject equippedItem;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.EquipItem>(OnEquipItem);
        EventBus.Instance.Subscribe<GameEvents.UnequipItem>(_ => OnUnequipItem());

    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.EquipItem>(OnEquipItem);
        EventBus.Instance.Unsubscribe<GameEvents.UnequipItem>(_ => OnUnequipItem());
    }

    public void OnEquipItem(GameEvents.EquipItem evt) {
        if (evt.itemID != 0){ 
            StartCoroutine(LoadPickableItem(evt.itemID));
        }
        Debug.Log($"불러올 아이템ID : {evt.itemID}");
    }

    private IEnumerator LoadPickableItem(int itemID) {
        handle = Addressables.LoadAssetAsync<GameObject>(itemID.ToString());
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject item = handle.Result;

            // 아이템 생성
            equippedItem = Instantiate(item, leftDummy);

            // 아이템정렬
            equippedItem.transform.localPosition = Vector3.zero;
            equippedItem.transform.localRotation = Quaternion.identity;

            // 아이템 저장
            ItemManager.EquipItem = ItemDatabaseManager.Instance.GetPickable(itemID);
        }
        else {
            Debug.Log($"EquipItem - Failed to Load Pickable Addressable : {itemID}");
        }
    }

    public void OnUnequipItem() {
        // 장착 해제
        ItemManager.EquipItem = null;
        Release();
        Destroy(equippedItem);
    }

    private void Release() {
        if (handle.IsValid()) {
            Addressables.Release(handle);
        }
    }
}
