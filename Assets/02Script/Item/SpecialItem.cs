using UnityEngine;
using cakeslice;


[RequireComponent(typeof(Outline))]
public class SpecialItem : MonoBehaviour, IActionItem
{
    public int itemID;
    private SpecialData data;

    public int GetItemID() => itemID;
    ItemType IActionItem.GetType() => data.type;

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetSpecial(itemID);
        if (data == null)
            Debug.Log($"SpecialItem - Failed to Load {itemID} Data");

        if (TryGetComponent<Outline>(out Outline outline))
        {
            outline.color = 2;
        }
    }

    public void InteractOnClick()
    {
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, true));
    }

    public void InteractOnE()
    {
        // 이벤트 발생 + 특정분기
        EventBus.Instance.Publish<PuzzleEvents.ApproachSpecial>(new PuzzleEvents.ApproachSpecial(EventList.OpenClockMode));
    }

}
