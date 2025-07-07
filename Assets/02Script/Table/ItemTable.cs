using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resource/Table")]
public class ItemTable : ScriptableObject
{
	public List<PickableEntity> Pickable; 
	public List<InteractableEntity> Interactable;
	public List<InspectableEntity> Inspectable;
	public List<ReadableEntity> Readable;
}
