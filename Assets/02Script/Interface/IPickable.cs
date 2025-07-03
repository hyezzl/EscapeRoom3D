using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable : IActionItem
{
    void OnpickUp();
}
