using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Table")]
public class EventAfter : ScriptableObject
{
	public List<EventAfterEntity> eventAfter; // Replace 'EntityType' to an actual type that is serializable.
}
