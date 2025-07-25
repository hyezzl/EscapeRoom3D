using cakeslice;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ReadableItem : MonoBehaviour, IReadable
{
    public int itemID;
    private ReadableData data;

    public int GetItemID() => itemID;
    ItemType IActionItem.GetType() => data.type;

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetReadable(itemID);
        if (data == null)
            Debug.Log($"ReadableItem - Failed to Load {itemID} Data");
        if (TryGetComponent<Outline>(out Outline outline))
        {
            outline.color = 1;
        }
    }
    public void InteractOnClick()
    {
        // Monologue 재생
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, true));
    }

    public void InteractOnE()
    {
        // Narrative 재생
        EventBus.Instance.Publish<UIEvents.OpenNarrativePopup>(new UIEvents.OpenNarrativePopup(ItemManager.CurrentItem));
    }
}