using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private Transform leftDummy;
    private AsyncOperationHandle<GameObject> handle;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.EquipItem>(OnEquipItem);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.EquipItem>(OnEquipItem);
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
            GameObject obj = Instantiate(item, leftDummy);

            // 아이템정렬
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
        else {
            Debug.Log($"EquipItem - Failed to Load Pickable Addressable : {itemID}");
        }
    }

    private void Release() {
        if (handle.IsValid()) {
            Addressables.Release(handle);
        }
    }
}
