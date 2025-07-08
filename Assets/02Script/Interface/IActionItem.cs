using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionItem
{
    int GetItemID();
    ItemType GetType();

    void InteractOnClick();

    void InteractOnE();
}
