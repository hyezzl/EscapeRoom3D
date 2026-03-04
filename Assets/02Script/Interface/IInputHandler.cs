using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    Vector2 GetMovement();
    bool Run();
    bool Crouch();
    bool DoInsteract();
    bool ToggleLight();
    bool ToggleInventory();
    bool LeftClick();
    bool Escape();
}
