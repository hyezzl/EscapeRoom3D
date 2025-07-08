using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class OpenableItem : MonoBehaviour, IActionItem
{

    ItemType IActionItem.GetType() => ItemType.Openable;


    public int GetItemID() { return 0; }
    public void InteractOnClick() { }
    public void InteractOnE() { }
    
}
