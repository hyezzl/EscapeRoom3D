using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Table")]
public class EscapeTable : ScriptableObject
{
	public List<ObjectEntity> Object;
	public List<DialogueEntity> Dialogue;
}
