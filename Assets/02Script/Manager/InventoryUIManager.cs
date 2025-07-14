using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : PanelController
{
    [Header("UI Refs")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button exitBTN;

    private PlayerController pc;
    private const int slotCnt = 16;
    private List<InventoryUISlot> slots = new();

    protected override void Awake()
    {
        base.Awake();
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null)
            Debug.Log("InventoryUIManager - Failed to Load PlayerController");

        // 슬롯 생성
        for (int i = 0; i < slotCnt; i++)
        {
            var slot = Instantiate(slotPrefab, slotParent).GetComponent<InventoryUISlot>();
            slots.Add(slot);
        }
        RefreshInventory();
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.InventoryChanged>(OnInventoryChanged);
        EventBus.Instance.Subscribe<UIEvents.ToggleInventory>(_ => TogglePanel());
        EventBus.Instance.Subscribe<UIEvents.SlotClicked>(SelectSlot);
        exitBTN.onClick.AddListener(ExitInventory);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.InventoryChanged>(OnInventoryChanged);
        EventBus.Instance.Unsubscribe<UIEvents.ToggleInventory>(_ => TogglePanel());
        EventBus.Instance.Unsubscribe<UIEvents.SlotClicked>(SelectSlot);
    }

    private void OnInventoryChanged(GameEvents.InventoryChanged evt) {
        RefreshInventory();
    }

    public void RefreshInventory() {
        var items = InventoryManager.Instance.GetInventory();
        Debug.Log($"인벤토리 업데이트 : 현재 총 {items.Count}개"); // 주울 수 있는 아이템의 최대값 < slotCnt

        // 슬롯에 인벤토리 정보 전달
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                slots[i].Set(items[i]);
            }
            else {
                slots[i].Set(null);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ExitInventory();
        }
    }

    public override void TogglePanel()
    {
        base.TogglePanel();
        if (isPanelOpen)
        {
            //Debug.Log("인벤토리 열림");
            pc.CurMode = PlayMode.InventoryMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
        }
        else {
            //Debug.Log("인벤토리 닫힘");
            pc.CurMode = PlayMode.InspectMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
        }
    }

    // 선택된 슬롯 고유성
    public void SelectSlot(UIEvents.SlotClicked evt) {
        if (evt.itemInst == null) return; // 빈 슬롯일 경우 선택 불가
        ItemManager.SelectedSlot?.SetSelect(false); // 현재 선택된 슬롯 선택취소
        ItemManager.SelectedSlot = evt.slot;
        ItemManager.SelectedSlot?.SetSelect(true);

        Debug.Log($"선택아이템 : {ItemManager.SelectedSlot.ItemInst.itemID}");
    }

    // 인벤토리 닫기
    public void ExitInventory()
    {
        if (isPanelOpen)
        {
            pc.CurMode = PlayMode.InspectMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
            Hide();
        }
    }
}
