using cakeslice;
using System.Collections;
using System.Collections.Generic;
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
        if (TryGetComponent<Outline>(out Outline outline)) {
            outline.color = 1;
        }
    }
    public void PressEMessage()
    {

    }

    private void PlayNarrative() {
        Debug.Log($"내용 : {data.narrative}");
    }

    private void PlayMonologue() {
        Debug.Log($"독백 : {data.monologue}");
    }

    private void PlayReply() {
        Debug.Log($"Reply : {data.reply}");
    }

    public void InteractOnClick()
    {
        // Monologue 재생
        PlayMonologue();
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, true));
    }

    public void InteractOnE()
    {
        // Narrative 재생
        PlayNarrative();
        EventBus.Instance.Publish<UIEvents.OpenNarrativePopup>(new UIEvents.OpenNarrativePopup(ItemManager.CurrentItem));

        // Reply 재생
        // false값으로 할지? 아니면 추가로직할지
        PlayReply();
    }
}
