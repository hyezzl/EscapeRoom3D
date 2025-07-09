using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : PanelController
{
    [Header("UI Refs")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;

    private PlayerController pc;
    private const int slotCnt = 16;
    private List<InventoryUISlot> slots = new();

    public bool IsOpenInventory => isOpenInventory; // Getter

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
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.InventoryChanged>(OnInventoryChanged);
        EventBus.Instance.Subscribe<UIEvents.ToggleInventory>(_ => TogglePanel());
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.InventoryChanged>(OnInventoryChanged);
        EventBus.Instance.Unsubscribe<UIEvents.ToggleInventory>(_ => TogglePanel());
    }

    private void OnInventoryChanged(GameEvents.InventoryChanged evt) {
        RefreshInventory();
    }

    public void RefreshInventory() {
        var items = InventoryManager.Instance.GetInventory();
        Debug.Log($"인벤토리 변경 : 현재 총 {items.Count}개");

        // 슬롯에 인벤토리 정보 전달
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
                slots[i].Set(items[i]);
            else
                slots[i].Set(null);
        }
    }

    public override void TogglePanel()
    {
        // 인벤토리 토글 조건
        base.TogglePanel();
        if (isOpenInventory)
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
}
