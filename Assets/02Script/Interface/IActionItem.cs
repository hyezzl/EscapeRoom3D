using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionItem
{
    ItemType GetType();

    void InteractOnClick();

    void InteractOnE();
}
