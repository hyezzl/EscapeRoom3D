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
    [SerializeField] private CanvasGroup inform;

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

    private void Start()
    {
        inform.alpha = 0f; // inform 숨기기
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
        exitBTN.onClick.RemoveListener(ExitInventory);
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
        if (isPanelOpen) { 
            if (Input.GetKeyDown(KeyCode.Escape)) {
                ExitInventory();
            }

            // 슬롯이 선택된 경우
            if (ItemManager.SelectedSlot != null) {
                // View Mode
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("뷰모드 진입");

                    //모드변경
                    pc.CurMode = PlayMode.ViewMode;
                    EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

                    // 뷰모드 활성화
                    EventBus.Instance.Publish<UIEvents.OpenViewMode>(new UIEvents.OpenViewMode(ItemManager.SelectedSlot.ItemInst.itemID));              
                    
                }

                // Equip 
                if (Input.GetKeyDown(KeyCode.E)) {
                    Debug.Log("장착");
                    // 모드변경 + 인벤토리 닫기
                    ExitInventory();

                    // 아이템 Equip
                    if (ItemManager.SelectedSlot.ItemInst.itemID == 0) Debug.Log("선택된 아이템 없음");
                    EventBus.Instance.Publish<GameEvents.EquipItem>(new GameEvents.EquipItem(ItemManager.SelectedSlot.ItemInst.itemID));
                }
            }
        
        }
    }

    public override void TogglePanel()
    {
        base.TogglePanel();
        if (isPanelOpen)
        {
            pc.CurMode = PlayMode.InventoryMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
        }
        else {
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
