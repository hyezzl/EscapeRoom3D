using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    public Vector2 GetMovement();

    public bool Run();

    public bool DoInteractive();
}
