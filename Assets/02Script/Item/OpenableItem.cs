using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableItem : MonoBehaviour, IActionItem
{
    ItemType IActionItem.GetType()
    {
        return ItemType.Openable;
    }
}
