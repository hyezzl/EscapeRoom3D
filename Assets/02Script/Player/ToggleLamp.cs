using UnityEngine;

public class ToggleLamp : MonoBehaviour
{
    private IInputHandler inputHandler;
    private EquipLight lamp;

    private bool isLampOn = true;  // todo :: 후에 기본값 바꿔줄것 !!!

    private void Awake()
    {
        if (!TryGetComponent<IInputHandler>(out inputHandler))
        {
            Debug.Log("ToggleLamp - Failed to Load IInputHandler");
        }
        lamp = GetComponentInChildren<EquipLight>();
        if (lamp == null)
            Debug.Log("ToggleLamp - Failed to Load EquipLight");
    }

    private void Update()
    {
        Toggle();
    }

    private void Toggle()
    {
        if (inputHandler.ToggleLight())
        {
            if (isLampOn)
            {
                isLampOn = false;
                lamp.PutDownLamp();
            }
            else
            {
                isLampOn = true;
                lamp.PickupLamp();
            }
        }
    }
}
