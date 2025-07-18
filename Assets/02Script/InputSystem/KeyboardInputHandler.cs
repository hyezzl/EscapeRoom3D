using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour, IInputHandler
{
    public Vector2 GetMovement()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public bool Run()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool Crouch() {
        return Input.GetKey(KeyCode.LeftControl);
    }

    public bool DoInsteract()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool ToggleLight() {
        return Input.GetKeyDown(KeyCode.F);
    }

    public bool LeftClick() {
        return Input.GetMouseButtonUp(0);
    }

    public bool ToggleInventory() {
        return Input.GetKeyDown(KeyCode.I);
    }

    public bool Escape() {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
