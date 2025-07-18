using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ViewModeManager : Singleton<ViewModeManager>
{
    [SerializeField] private Canvas viewCanvas;
    private PickableItem item;
    private AsyncOperationHandle<GameObject> handle;
    private Vector3 spawnPos = new Vector3(0, 0, 2);


    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenViewMode>(OnViewMode);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenViewMode>(OnViewMode);
    }

    public void OnViewMode(UIEvents.OpenViewMode evt) {
        //evt.itemID;
    }
}
