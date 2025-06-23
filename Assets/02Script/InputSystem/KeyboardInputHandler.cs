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

    public bool DoInteractive()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
