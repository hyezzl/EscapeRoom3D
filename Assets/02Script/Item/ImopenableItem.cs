using cakeslice;
using UnityEngine;

public enum Imopenable
{ 
    Drawer,
    Door,
}

[RequireComponent(typeof(Outline))]
public class ImopenableItem : MonoBehaviour, IActionItem
{
    //[SerializeField] private AudioSource audio;
    [SerializeField] private Imopenable type;


    ItemType IActionItem.GetType() => ItemType.Imopenable;

    public int GetItemID() { return 0; }

    private void Start()
    {
        if (TryGetComponent<Outline>(out Outline outline))
        {
            outline.color = 2;
        }
    }

    public void InteractOnClick()
    {
        // 열리지 않는 소리
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, true));
    }

    public void InteractOnE()
    {
        // "문"을 위한 특별 루트
        if (type == Imopenable.Door && ItemManager.EquipItem != null)
        {
            EventBus.Instance.Publish<GameEvents.KnockDoor>(new GameEvents.KnockDoor(ItemManager.EquipItem.pairID));
        }
        else { 
            // 열리지 않는 소리
            EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, false));
        }
    }

}
