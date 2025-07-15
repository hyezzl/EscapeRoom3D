using UnityEngine;

public class ViewItem : MonoBehaviour
{
    private PickableItem item;



    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenViewMode>(OnViewMode);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenViewMode>(OnViewMode);
    }

    public void OnViewMode(UIEvents.OpenViewMode evt) {
        Debug.Log("ºä¸ðµå ½ÇÇà");
        //evt.itemID;
    }
}
