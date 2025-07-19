using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ViewModeManager : Singleton<ViewModeManager>
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas viewCanvas;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Button exitBTN;

    private GameObject item;
    private AsyncOperationHandle<GameObject> handle;
    private PlayerController pc;

    protected override void DoAwake()
    {
        base.DoAwake();
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) Debug.Log("ViewModeManager - Failed to Load PlayerController");

        viewCanvas.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenViewMode>(OnViewMode);
        exitBTN?.onClick.AddListener(ExitViewMode);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenViewMode>(OnViewMode);
        exitBTN?.onClick.RemoveListener(ExitViewMode);
    }

    public void OnViewMode(UIEvents.OpenViewMode evt) {
        // 캔버스 변경
        mainCanvas?.gameObject.SetActive(false);
        viewCanvas?.gameObject.SetActive(true);

        // 아이템 로드/생성
        if (evt.itemID != 0) {
            StartCoroutine(LoadPickableItem(evt.itemID));
        }
    }

    private IEnumerator LoadPickableItem(int itemID) {
        handle = Addressables.LoadAssetAsync<GameObject>(itemID.ToString());
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded) {
            GameObject addr = handle.Result;

            // 메인카메라 앞에 오브젝트 생성
            Vector3 dir = mainCam.transform.forward;
            Vector3 spawnPos = mainCam.transform.position + (dir * 1f);

            item = Instantiate(addr, spawnPos, mainCam.transform.rotation, mainCam.transform);
        }
    }

    private void Release() {
        if (handle.IsValid()) {
            Addressables.Release(handle);
        }
    }

    private void ExitViewMode() {
        // 캔버스 교체
        viewCanvas?.gameObject.SetActive(false);
        mainCanvas?.gameObject.SetActive(true);

        // 모드 종료
        pc.CurMode = PlayMode.InventoryMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
        EventBus.Instance.Publish<UIEvents.CloseViewMode>(new UIEvents.CloseViewMode());

        // 해제
        if (item != null) { 
            Destroy(item);
            item = null;
        }
        Release();
    }
}
