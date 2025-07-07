using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableItem : MonoBehaviour, IActionItem
{

    ItemType IActionItem.GetType() => ItemType.Openable;


    public void InteractOnClick() { }
    public void InteractOnE() { }
    
}
