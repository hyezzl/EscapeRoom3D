using UnityEngine;

public class PulseIcon : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim)) {
            Debug.Log("PulseIcon - Failed to Load Animator");
        }
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.SlotClicked>(OnSlotClicked);
        EventBus.Instance.Subscribe<UIEvents.ToggleInventory>(OnToggleInventory);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.SlotClicked>(OnSlotClicked);
        EventBus.Instance.Unsubscribe<UIEvents.ToggleInventory>(OnToggleInventory);
    }

    private void OnSlotClicked(UIEvents.SlotClicked evt) {
        anim.SetBool("isPulse", true);
    }

    private void OnToggleInventory(UIEvents.ToggleInventory evt)
    {
        anim.SetBool("isPulse", false);
    }
}
