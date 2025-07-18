using UnityEngine;

public class ToggleInventory : MonoBehaviour
{
    private PlayerController pc;
    private IInputHandler inputHandler;

    private void Awake()
    {
        if (!TryGetComponent<PlayerController>(out pc)) {
            Debug.Log("ToggleInventory - Failed to Load PlayerController");
        }
        if (!TryGetComponent<IInputHandler>(out inputHandler))
        {
            Debug.Log("ToggleInventory - Failed to Load IInputHandler");
        }
    }

    private void Update()
    {
        Toggle();
    }

    private void Toggle()
    {
        if (inputHandler.ToggleInventory())
        {
            if (pc.CurMode == PlayMode.InspectMode || pc.CurMode == PlayMode.InventoryMode)
            {
                EventBus.Instance.Publish<UIEvents.ToggleInventory>(new UIEvents.ToggleInventory());
            }
        }
    }
}
