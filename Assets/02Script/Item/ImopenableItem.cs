using cakeslice;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ImopenableItem : MonoBehaviour, IActionItem
{
    [SerializeField] private AudioSource audio;

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
        // 열리지 않는 소리
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, false));
    }

    
}
