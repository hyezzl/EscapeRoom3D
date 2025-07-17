using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : PanelController
{
    [Header("UI Refs")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Button exitBTN;

    [Header("Inform Message")]
    [SerializeField] private TextMeshProUGUI equipText;
    [SerializeField] private TextMeshProUGUI unequipText;
    [SerializeField] private CanvasGroup staticgroup;

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
        equipText.enabled = true; // 기본문구
        unequipText.enabled = false;
    }


    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.InventoryChanged>(OnInventoryChanged);
        EventBus.Instance.Subscribe<UIEvents.ToggleInventory>(OnToggleInventory);
        EventBus.Instance.Subscribe<UIEvents.SlotClicked>(SelectSlot);
        exitBTN.onClick.AddListener(ExitInventory);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.InventoryChanged>(OnInventoryChanged);
        EventBus.Instance.Unsubscribe<UIEvents.ToggleInventory>(OnToggleInventory);
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

                if (ItemManager.EquipItem != null && 
                    items[i].itemID == ItemManager.EquipItem.itemID)
                {  // 장착중인 아이템일 때
                    slots[i].SetEquip(true);
                }
                else { 
                    slots[i].SetEquip(false);
                }
            }
            else {
                slots[i].Set(null);
                slots[i].SetEquip(false);
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
                // 선택 슬롯 정보 캐싱
                var selectedSlotcache = ItemManager.SelectedSlot;

                // View Mode
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("뷰모드 진입");

                    //모드변경
                    pc.CurMode = PlayMode.ViewMode;
                    EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

                    // 뷰모드 활성화
                    EventBus.Instance.Publish<UIEvents.OpenViewMode>(new UIEvents.OpenViewMode(selectedSlotcache.ItemInst.itemID));              
                    
                }

                // Equip (장착 / 해제)
                if (Input.GetKeyDown(KeyCode.E)) {
                    // 모드변경 + 인벤토리 닫기
                    ExitInventory();

                    if (ItemManager.EquipItem == null)
                    {
                        // 아이템 Equip
                        EventBus.Instance.Publish<GameEvents.EquipItem>(new GameEvents.EquipItem(selectedSlotcache.ItemInst.itemID));
                    }
                    else {
                        // 선택된 아이템이 이미 착용하고 있는 아이템 (장착 해제)
                        if (ItemManager.EquipItem.itemID == selectedSlotcache.ItemInst.itemID)
                        {
                            EventBus.Instance.Publish<GameEvents.UnequipItem>(new GameEvents.UnequipItem());
                            RefreshInventory(); // :( 박스 바로 사라지는게 보기좋음
                        }
                        else // 착용하고 있는 아이템과는 다른 아이템 Equip (해제 후 장착)
                        {
                            EventBus.Instance.Publish<GameEvents.UnequipItem>(new GameEvents.UnequipItem());
                            RefreshInventory();
                            EventBus.Instance.Publish<GameEvents.EquipItem>(new GameEvents.EquipItem(selectedSlotcache.ItemInst.itemID));
                        }
                    }
                }
            }
        
        }
    }

    public void OnToggleInventory(UIEvents.ToggleInventory evt) {
        RefreshInventory();
        TogglePanel();
        equipText.enabled = true;
        unequipText.enabled = false;

        if (isPanelOpen)
        {
            pc.CurMode = PlayMode.InventoryMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
        }
        else
        {
            pc.CurMode = PlayMode.InspectMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
            
            // 선택 슬롯 해제
            if (ItemManager.SelectedSlot != null)
            {
                ItemManager.SelectedSlot?.SetSelect(false);
                ItemManager.SelectedSlot = null;
            }
        }
    }

    // 선택된 슬롯 고유성
    public void SelectSlot(UIEvents.SlotClicked evt) {

        if (evt.itemInst == null) return; // 빈 슬롯일 경우 선택 불가

        // focus + SelectedSlot 저장
        ItemManager.SelectedSlot?.SetSelect(false); // 현재 선택된 슬롯 선택취소
        ItemManager.SelectedSlot = evt.slot;
        ItemManager.SelectedSlot?.SetSelect(true);
        Debug.Log($"선택아이템 : {ItemManager.SelectedSlot.ItemInst.itemID}");

        // 장착중인 아이템을 선택했다면,
        if(ItemManager.EquipItem != null && ItemManager.SelectedSlot !=null &&
        ItemManager.EquipItem.itemID == ItemManager.SelectedSlot.ItemInst.itemID)
        {
            equipText.enabled = false;
            unequipText.enabled = true;
        }
        else {
            equipText.enabled = true;
            unequipText.enabled = false;
        }
    }

    // 인벤토리 닫기
    public void ExitInventory()
    {
        if (isPanelOpen)
        {
            pc.CurMode = PlayMode.InspectMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
            Hide();

            // 선택 슬롯 해제
            if (ItemManager.SelectedSlot != null) { 
                ItemManager.SelectedSlot?.SetSelect(false);
                ItemManager.SelectedSlot = null;
            }
        }
    }
}
