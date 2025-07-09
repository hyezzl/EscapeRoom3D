using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : PanelController
{
    [Header("UI Refs")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;

    private const int slotCnt = 16;
    private List<InventoryUISlot> slots = new();

    public bool IsOpenInventory => isOpenInventory; // Getter

    protected override void Awake()
    {
        base.Awake();

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
}
