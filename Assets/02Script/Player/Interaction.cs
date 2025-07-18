using UnityEngine;

public class Interaction : MonoBehaviour
{
    private PlayerController pc;
    private IInputHandler inputHandler;


    private void Awake()
    {
        if (!TryGetComponent<PlayerController>(out pc)) {
            Debug.Log("Interaction - Failed to Load PlayerController");
        }
        if (!TryGetComponent<IInputHandler>(out inputHandler))
        {
            Debug.Log("Interaction - Failed to Load IInputHandler");
        }
    }

    private void Update()
    {
        OnClick();
        PressE();
    }

    private void OnClick()
    {
        if (ItemManager.CurrentItem == null) return;
        if (pc.CurMode == PlayMode.InspectMode && inputHandler.LeftClick())
        {
            ItemManager.CurrentItem.InteractOnClick();
        }
    }

    private void PressE()
    {
        if (ItemManager.CurrentItem == null) return;
        if (pc.CurMode == PlayMode.InspectMode && inputHandler.DoInsteract())
        {
            ItemManager.CurrentItem.InteractOnE();
        }
    }
}
